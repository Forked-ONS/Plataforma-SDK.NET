using System.Collections.Generic;
using System.Linq;
using ONS.SDK.Domain.Core;
using ONS.SDK.Utils.Http;
using Plataforma_SDK.NET.ONS.SDK.Domain.Core;
using Plataforma_SDK.NET.ONS.SDK.Domain.Services;

namespace ONS.SDK.Platform.Domain {
    public class DomainService {
        private string _systemId;
        private string _instanceId;
        private JsonHttpClient _client;
        private InstalledApp _info;
        private string _branch;

        public DomainService (ApplicationContext appContext, JsonHttpClient client, IInstalledAppService installedAppService) {
            this._systemId = appContext.SystemId;
            this._instanceId = appContext.ProcessInstanceId;
            this._client = client;
            this._info = installedAppService.FindBySystemIdAndType (this._systemId, "domain").FirstOrDefault ();
            this._branch = "master";
        }

        public DomainService OnBranch (string branch) {
            if (branch != "") {
                this._branch = branch;
            }
            return this;
        }

        public T FindById<T> (string map, string type, string id) {
            var url = $"{this.Url}/{map}/{type}?filter=byId&id={id}";
            return this._client.Get<List<T>> (url,
                    new Header () { Key = "Branch", Value = this._branch },
                    new Header () { Key = "Instance-Id", Value = this._instanceId })
                .FirstOrDefault ();
        }

        public List<T> Query<T>() {
            //://${o.host}:${o.port}/${obj._map}/${obj._entity}${query}
        }

        private string Url => $"http://{this._info.Host}:{this._info.Port}";
    }
}