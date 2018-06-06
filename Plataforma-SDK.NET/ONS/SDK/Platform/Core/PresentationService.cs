using ONS.SDK.Infra;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.Core
{
    public class PresentationService : CoreService
    {
        public PresentationService(CoreConfig config, JsonHttpClient client) : base(config, client, "presentation")
        {
        }
    }
}