using System.Collections.Generic;
using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.Core
{
    public class OperationService : CoreService
    {
        public OperationService(CoreConfig config, JsonHttpClient client) : base(config, client, "operation")
        {
        }

        public List<Operation> FindByEventInAndSystemId(string systemId, string eventIn){
            var criteria = new Criteria () {
                FilterName = "bySystemIdAndEventIn",
                Parameters = new List<Parameter> ()
                {
                new Parameter ()
                    {
                        Name = "systemId", Value = systemId
                    },
                new Parameter ()
                    {
                        Name = "event", Value = eventIn
                    }
                }
            };
            return this.Find<Operation>(criteria);
        }
    }
}