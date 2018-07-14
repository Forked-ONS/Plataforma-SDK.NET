using ONS.SDK.Configuration;
using ONS.SDK.Services;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Impl.Services.Core
{
    public class SystemService : CoreService<ONS.SDK.Domain.Core.System>, ISystemService
    {
        public SystemService(CoreConfig config, JsonHttpClient client) : base(config, client, "system")
        {
        }
    }
}