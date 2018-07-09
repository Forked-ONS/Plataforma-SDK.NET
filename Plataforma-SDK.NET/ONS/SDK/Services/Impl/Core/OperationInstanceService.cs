using System.Collections.Generic;
using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Services.Impl.Core
{
    public class OperationInstanceService : CoreService<OperationInstance>, IOperationInstanceService
    {
        public OperationInstanceService(CoreConfig config, JsonHttpClient client) : base(config, client, "operationInstance")
        {

        }
    }
}