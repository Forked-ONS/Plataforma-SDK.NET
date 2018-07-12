using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ONS.SDK.Context;
using ONS.SDK.Domain.Base;
using ONS.SDK.Domain.Core;
using ONS.SDK.Services;
using ONS.SDK.Services.Domain;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Services.Impl.Domain 
{

    public class DomainService: IDomainService {
                
        private JsonHttpClient _client;
        
        private IExecutionContext _executionContext;

        private string _instanceId {
            get {
                return _executionContext.ExecutionParameter.InstanceId;
            }
        }

        private string _branch {
            get {
                return _executionContext.ExecutionParameter.Branch;
            }
        }

        private string _urlDomainServiceLocal {
            get {
                return System.Environment.GetEnvironmentVariable("URL_DOMAIN_SERVICE_LOCAL");
            }
        }

        private readonly string _url;

        public DomainService (IExecutionContext executionContext, JsonHttpClient client, IInstalledAppService installedAppService) {
            
            this._executionContext = executionContext;
            this._client = client;
            
            var urlDomainServiceLocal = _urlDomainServiceLocal;
            if (!string.IsNullOrEmpty(urlDomainServiceLocal)) {
                _url = urlDomainServiceLocal;
            } else {
                var systemId = executionContext.SystemId;
                var info = installedAppService.FindBySystemIdAndType(systemId, "domain").FirstOrDefault();
                _url = $"http://{info.Host}:{info.Port}";
            }

        }

        public T FindById<T>(string map, string type, string id) where T: Model {
            var url = $"{this._url}/{map}/{type}?filter=byId&id={id}";
            return this._client.Get<List<T>> (url,
                    new Header () { Key = "Branch", Value = this._branch },
                    new Header () { Key = "Instance-Id", Value = this._instanceId })
                .FirstOrDefault ();
        }

        public List<T> QueryByFilter<T>(Filter filter) where T: Model {

            return Query<T>(filter.Map, filter.Entity, filter.Name, filter.Parameters);
        }

        public List<T> Query<T>(string map, string type, 
            string filterName = null, IDictionary<string, object> parameters = null) where T: Model
        {
            var url = $"{this._url}/{map}/{type}";
            
            if (!string.IsNullOrEmpty(filterName)) {
                url += $"?filter={filterName}";
            }
            if (parameters != null && parameters.Count > 0) {
                foreach (var parameter in parameters)
                {
                    url += $"&{parameter.Key}={parameter.Value}";
                }
            }

            return this._client.Get<List<T>>(url,
                    new Header () { Key = "Branch", Value = this._branch },
                    new Header () { Key = "Instance-Id", Value = this._instanceId });
        }

        public void Persist(string map, IList<Model> entities) {
            
            var url = $"{this._url}/{map}/persist";

            this._client.Post<object>(url, entities,
                    new Header () { Key = "Branch", Value = this._branch },
                    new Header () { Key = "Instance-Id", Value = this._instanceId });
        }
    }
}