using ONS.SDK.Infra;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.Core
{
    public class MapService : CoreService
    {
        public MapService(CoreConfig config, JsonHttpClient client) : base(config, client, "map")
        {
        }
    }
}