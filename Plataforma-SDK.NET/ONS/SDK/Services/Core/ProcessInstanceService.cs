using ONS.SDK.Configuration;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.Core
{
    public class ProcessInstanceService : CoreService
    {
        public ProcessInstanceService(CoreConfig config, JsonHttpClient client) : base(config, client, "processInstance")
        {
        }
    }
}