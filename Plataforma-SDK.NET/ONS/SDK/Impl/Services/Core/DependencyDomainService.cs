using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Services;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Impl.Services.Core
{
    public class DependencyDomainService : CoreService<DependencyDomain>, IDependencyDomainService
    {
        public DependencyDomainService(CoreConfig config, JsonHttpClient client) : base(config, client, "dependencyDomain")
        {
        }
    }
}