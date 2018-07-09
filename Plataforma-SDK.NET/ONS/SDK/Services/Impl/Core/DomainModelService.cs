using System.Collections.Generic;
using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Services.Impl.Core
{
    public class DomainModelService : CoreService<DomainModel>, IDomainModelService
    {
        public DomainModelService(CoreConfig config, JsonHttpClient client) : base(config, client, "domainModel")
        {
        }

        public List<DomainModel> FindBySystemIdAndName(string systemId, string name) {
            var criteria = new Criteria () {
                FilterName = "bySystemIdAndName",
                Parameters = new List<Parameter> ()
                {
                new Parameter ()
                    {
                        Name = "systemId", Value = systemId
                    },
                new Parameter ()
                    {
                        Name = "name", Value = name
                    }
                }
            };
            return this.Find(criteria);
        }

    }
}