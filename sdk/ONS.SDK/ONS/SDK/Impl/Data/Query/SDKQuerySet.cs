

using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ONS.SDK.Configuration;
using ONS.SDK.Context;
using ONS.SDK.Data.Query;
using ONS.SDK.Domain.Base;
using ONS.SDK.Logger;
using ONS.SDK.Services.Domain;
using ONS.SDK.Worker;
using System;
using System.Linq;

namespace ONS.SDK.Impl.Data.Query
{
    public class SDKQuerySet<T> : IQuerySet<T> where T: Model
    {
        private readonly ILogger<SDKQuerySet<T>> _logger;

        private readonly string _mapName;

        private readonly string _entityName;

        private readonly IDictionary<string, IList<string>> _filters;

        private readonly IDomainService _domainService;

        private readonly IExecutionContext _executionContext;

        public SDKQuerySet(string mapName, string entityName, IDictionary<string, IList<string>> filters) 
        {
            this._logger = SDKLoggerFactory.Get<SDKQuerySet<T>>();
            this._mapName = mapName;
            this._entityName = entityName;
            this._domainService = SDKConfiguration.ServiceProvider.GetService<IDomainService>();
            this._executionContext = SDKConfiguration.ServiceProvider.GetService<IExecutionContext>();
            this._filters = filters;
        }

        public IList<T> All()
        {
            return this._domainService.Query<T>(_mapName, _entityName);
        }

        public bool Any(string filterName = null, object filter = null)
        {
            // TODO o ideal seria ter uma opção de count do domain
            
            return Find(filterName, filter).Any();
        }

        public bool Any(IQueryFilter filter)
        {
            // TODO o ideal seria ter uma opção de count do domain

            return Find(filter).Any();
        }

        public T ById(string id)
        {
            return this._domainService.FindById<T>(_mapName, _entityName, id);
        }

        public int Count(string filterName = null, object filter = null)
        {
            // TODO o ideal seria ter uma opção de count do domain
            
            return Find(filterName, filter).Count();
        }

        public int Count(IQueryFilter filter)
        {
            // TODO o ideal seria ter uma opção de count do domain
            
            return Find(filter).Count();
        }

        private void _validateFilter(string filterName) {
            if (string.IsNullOrEmpty(filterName)) {
                throw new SDKRuntimeException("Filter name is required.");
            }
            Console.WriteLine(">>>>>>>>filters:" + Newtonsoft.Json.JsonConvert.SerializeObject(_filters));
            if (!_filters.ContainsKey(filterName)) {
                throw new SDKRuntimeException($"Filter not found in map of process. filterName:{filterName}");
            }
        }

        private void _validateFilter(IQueryFilter filter) {
            if (filter == null) {
                throw new SDKRuntimeException("Filter is required.");
            }
            _validateFilter(filter.Name);
        }

        public IList<T> Find(string filterName = null, object filter = null)
        {
            _validateFilter(filterName);
            
            IDictionary<string, object> parameters = null;
            if (filter != null) {
                var parametersName = _filters[filterName];
                parameters = QueryHelper.MakeParameters(parametersName, filter);
            }

            return this._domainService.Query<T>(_mapName, _entityName, filterName, parameters);
        }

        public IList<T> Find(IQueryFilter filter)
        {
            _validateFilter(filter);
            
            IDictionary<string, object> parameters = null;
            if (filter.GetParameters() != null) {
                var parametersName = _filters[filter.Name];
                parameters = QueryHelper.MakeParameters(parametersName, filter.GetParameters());
            }

            return this._domainService.Query<T>(_mapName, _entityName, filter.Name, parameters);
        }

        public IPagedResult<T> FindPaged(IQueryPagedFilter filter) 
        {
            _validateFilter(filter);

            IDictionary<string, object> parameters = null;
            if (filter.GetParameters() != null) {
                var parametersName = _filters[filter.Name];
                Console.WriteLine("############# parametersName: " + Newtonsoft.Json.JsonConvert.SerializeObject(parametersName));
                parameters = QueryHelper.MakeParameters(parametersName, filter.GetParameters());
            }

            if (parameters == null) {
                parameters = new Dictionary<string, object>();
            }
            
            parameters.Add(SDKConstants.ParamNamePage, filter.Page);
            parameters.Add(SDKConstants.ParamNamePageSize, filter.PageSize);

            var result = this._domainService.Query<T>(_mapName, _entityName, filter.Name, parameters);
            // TODO percisará obter esse parâmetro
            var totalCount = 100;

            return new PagedResult<T>() {
                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalCount = totalCount,
                Result = result
            };
        }
    }
}