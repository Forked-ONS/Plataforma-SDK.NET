using System.Collections.Generic;
using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Services.Impl.Core
{
    public class PresentationInstanceService : CoreService<PresentationInstance>, IPresentationInstanceService
    {
        public PresentationInstanceService(CoreConfig config, JsonHttpClient client) : base(config, client, "presentationInstance")
        {
        }

    }
}