using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Services.Impl.Core
{
    public class DependencyDomainService : CoreService<DependencyDomain>, IDependencyDomainService
    {
        public DependencyDomainService(CoreConfig config, JsonHttpClient client) : base(config, client, "dependencyDomain")
        {
        }
    }
}