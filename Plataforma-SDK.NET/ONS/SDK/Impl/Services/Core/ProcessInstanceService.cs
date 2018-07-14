using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Services;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Impl.Services.Core
{
    public class ProcessInstanceService : CoreService<ProcessInstance>, IProcessInstanceService
    {
        public ProcessInstanceService(CoreConfig config, JsonHttpClient client) : base(config, client, "processInstance")
        {
        }
    }
}