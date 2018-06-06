using System.Collections.Generic;
using ONS.SDK.Domain.Core;
using ONS.SDK.Infra;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.Core
{
    public class OperationInstanceService : CoreService
    {
        public OperationInstanceService(CoreConfig config, JsonHttpClient client) : base(config, client, "operationInstance")
        {

        }

        public List<Operation> FindByProcessInstanceId(string processInstanceId) {
            var criteria = new Criteria () {
                FilterName = "byProcessInstanceId",
                Parameters = new List<Parameter> ()
                {
                new Parameter ()
                    {
                        Name = "processInstanceId", Value = processInstanceId
                    }
                }
            };
            return this.Find<Operation>(criteria);
        }

        public List<Operation> FindByOperationId(string operationId) {
            var criteria = new Criteria () {
                FilterName = "byOperationId",
                Parameters = new List<Parameter> ()
                {
                new Parameter ()
                    {
                        Name = "operationId", Value = operationId
                    }
                }
            };
            return this.Find<Operation>(criteria);
        }
    }
}