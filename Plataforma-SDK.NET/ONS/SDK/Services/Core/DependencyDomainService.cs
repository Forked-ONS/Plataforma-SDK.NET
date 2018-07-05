using ONS.SDK.Configuration;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.Core
{
    public class DependencyDomainService : CoreService
    {
        public DependencyDomainService(CoreConfig config, JsonHttpClient client) : base(config, client, "dependencyDomain")
        {
        }
    }
}