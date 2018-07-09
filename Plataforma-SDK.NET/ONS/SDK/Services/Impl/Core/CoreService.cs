using System.Collections.Generic;
using System.Linq;
using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Utils.Http;
using ONS.SDK.Domain.Base;
using ONS.SDK.Services;
using ONS.SDK.Services.Impl.Exceptions;

namespace ONS.SDK.Services.Impl.Core {

    public abstract class CoreService<T> : ICoreService<T> where T: Model {
        private CoreConfig _config;
        private JsonHttpClient _client;
        private string _entity;

        public CoreService (CoreConfig config, JsonHttpClient client, string entity) {
            this._config = config;
            this._client = client;
            this._entity = entity;
        }

        public void Save(List<Model> entities) {
            if (entities.Any (e => e._Metadata == null)) {
                throw new InvalidEntityException("Entity with no _metadata is not support");
            }
            this._client.Post<List<object>> ($"{this._config.Url}/core/persist", entities);
        }

        public void Save(Model entity) {
            this.Save(new List<Model>(){entity});
        }

        protected string buildFindQueryUrl(Criteria criteria) {
            var url = this._config.Url;
            url = $"{url}/core/{this._entity}?filter={criteria.FilterName}";
            foreach (var p in criteria.Parameters) {
                url += $"&{p.Name}={p.Value}";
            }
            return url;
        }

        public List<T> Find(Criteria criteria) {
            var url = this.buildFindQueryUrl(criteria);
            return this._client.Get<List<T>>(url);
        }

        public List<T> FindByName(string name) {
            var criteria = new Criteria () {
                FilterName = "byName",
                Parameters = new List<Parameter> ()
                { new Parameter ()
                    {
                        Name = "name", Value = name
                    }
                }
            };

            return this.Find(criteria);
        }

        public List<T> FindBySystemId(string id) {
            var criteria = new Criteria () {
                FilterName = "bySystemId",
                Parameters = new List<Parameter> ()
                { new Parameter ()
                    {
                        Name = "systemId", Value = id
                    }
                }
            };
            return this.Find(criteria);
        }

        public List<T> FindByProcessId(string id) {
            var criteria = new Criteria () {
                FilterName = "byProcessId",
                Parameters = new List<Parameter> ()
                { new Parameter ()
                    {
                        Name = "processId", Value = id
                    }
                }
            };
            return this.Find(criteria);
        }

        public List<T> FindById(string id) {
            var criteria = new Criteria () {
                FilterName = "byId",
                Parameters = new List<Parameter> ()
                { new Parameter ()
                    {
                        Name = "id", Value = id
                    }
                }
            };
            return this.Find(criteria);
        }

    }
}