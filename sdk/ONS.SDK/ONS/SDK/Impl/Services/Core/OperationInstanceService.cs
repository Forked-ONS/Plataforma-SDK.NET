using System.Collections.Generic;
using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Services;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Impl.Services.Core
{
    public class OperationInstanceService : CoreService<OperationInstance>, IOperationInstanceService
    {
        public OperationInstanceService(CoreConfig config, JsonHttpClient client) : base(config, client, "operationInstance")
        {

        }
    }
}