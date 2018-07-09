using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Services.Impl.Core
{
    public class ProcessInstanceService : CoreService<ProcessInstance>, IProcessInstanceService
    {
        public ProcessInstanceService(CoreConfig config, JsonHttpClient client) : base(config, client, "processInstance")
        {
        }
    }
}