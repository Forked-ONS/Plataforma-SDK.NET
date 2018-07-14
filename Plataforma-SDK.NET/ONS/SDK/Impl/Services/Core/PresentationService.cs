using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Services;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Impl.Services.Core
{
    public class PresentationService : CoreService<Presentation>, IPresentationService
    {
        public PresentationService(CoreConfig config, JsonHttpClient client) : base(config, client, "presentation")
        {
        }
    }
}