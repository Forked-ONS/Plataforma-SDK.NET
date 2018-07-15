using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using ONS.SDK.Configuration;
using ONS.SDK.Context;
using ONS.SDK.Data.Query;
using ONS.SDK.Domain.Base;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Services;
using ONS.SDK.Worker;

namespace ONS.SDK.Impl.Data.Query
{
    public class SDKDataQuery : IDataQuery
    {
        private readonly ILogger<SDKDataQuery> _logger;

        private readonly IExecutionContext _executionContext;

        private readonly IMapService _mapService;

        private readonly IDictionary<Type, IQuerySet> _queriesSet;

        public SDKDataQuery(ILogger<SDKDataQuery> logger, IExecutionContext executionContext, IMapService mapService) {
            this._logger = logger;
            this._executionContext = executionContext;
            this._mapService = mapService;

            this._queriesSet = new Dictionary<Type, IQuerySet>();
            this._loadQueriesSet();
        }

        public IQuerySet<T> Set<T>() where T : Model
        {
            var type = typeof(T);

            if (!this._queriesSet.ContainsKey(type)) {
                throw new SDKRuntimeException($"Type[{type.Name}] not found in map from process[{_executionContext.ProcessId}].");
            }

            return (IQuerySet<T>) this._queriesSet[type];
        }

        private void _loadQueriesSet() 
        {
            var processId = this._executionContext.ProcessId;

            var maps = this._mapService.FindByProcessId(processId);
                
            var map = maps.FirstOrDefault();
            if (map != null) 
            {    
                var processMap = ProcessMap.ConvertFromMap(map);

                this._loadQueriesSet(processMap);
            }
        }

        private void _loadQueriesSet(ProcessMap map) 
        {
            if (map != null && map.Content != null) 
            {
                var mapName = map.Name;

                foreach(var mapNameItem in map.Content) {

                    var entityName = mapNameItem.Key;
                    var mapItem = mapNameItem.Value;

                    var type = SDKDataMap.GetMap(entityName);

                    if (type != null) {

                        var filters = _loadFiltersFrom(mapItem);
                        
                        var queryset = (IQuerySet) Activator.CreateInstance(
                            typeof(SDKQuerySet<>).MakeGenericType(type), 
                            mapName, entityName, filters
                        );
    
                        _queriesSet[type] = queryset;

                    } else {  
                        this._logger.LogWarning($"Unregistered mapping type for map name. mapName: {mapName}");
                    }
                }
            }

        }

        private IDictionary<string, IList<string>> _loadFiltersFrom(MapItem mapItem) 
        {    
            var retorno = new Dictionary<string, IList<string>>();

            if (mapItem.Filters != null && mapItem.Filters.Any()) {
                foreach (var filter in mapItem.Filters)
                {
                    var filterName = filter.Key;
                    var query = filter.Value;

                    if (!string.IsNullOrEmpty(query)) {
                        retorno[filterName] = QueryHelper.GetParametersName(query);
                    }
                }
            }
            
            return retorno;
        }
    }
}