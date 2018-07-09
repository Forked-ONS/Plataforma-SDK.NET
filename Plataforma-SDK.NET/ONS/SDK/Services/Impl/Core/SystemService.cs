using ONS.SDK.Configuration;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Services.Impl.Core
{
    public class SystemService : CoreService<ONS.SDK.Domain.Core.System>, ISystemService
    {
        public SystemService(CoreConfig config, JsonHttpClient client) : base(config, client, "system")
        {
        }
    }
}