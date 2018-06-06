using ONS.SDK.Infra;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.Core
{
    public class SystemService : CoreService
    {
        public SystemService(CoreConfig config, JsonHttpClient client) : base(config, client, "system")
        {
        }
    }
}