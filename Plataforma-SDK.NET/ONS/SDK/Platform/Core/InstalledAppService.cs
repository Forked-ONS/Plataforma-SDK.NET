using System.Collections.Generic;
using ONS.SDK.Domain.Core;
using ONS.SDK.Infra;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.Core
{
    public class InstalledAppService : CoreService
    {
        public InstalledAppService(CoreConfig config, JsonHttpClient client) : base(config, client, "installedApp")
        {
        }


        public List<InstalledApp> FindBySystemIdAndType(string systemId, string type){
            var criteria = new Criteria () {
                FilterName = "bySystemIdAndType",
                Parameters = new List<Parameter> ()
                {
                new Parameter ()
                    {
                        Name = "systemId", Value = systemId
                    },
                new Parameter ()
                    {
                        Name = "type", Value = type
                    }
                }
            };
            return this.Find<InstalledApp>(criteria);
        }
    }
}