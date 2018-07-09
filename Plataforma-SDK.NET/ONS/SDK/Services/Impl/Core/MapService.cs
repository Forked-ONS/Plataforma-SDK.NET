using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Services.Impl.Core
{
    public class MapService : CoreService<Map>, IMapService
    {
        public MapService(CoreConfig config, JsonHttpClient client) : base(config, client, "map")
        {
        }
    }
}