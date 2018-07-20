using Microsoft.Extensions.Configuration;

namespace ONS.SDK.Configuration
{
    /// <summary>
    /// Classe que representa as configurações de serviços do core da plataforma.
    /// </summary>
    public class CoreConfig : BaseServiceConfig
    {
        public CoreConfig (IConfiguration conf) {
            this.Host = conf.GetValue ("COREAPI_HOST", "localhost");
            this.Scheme = conf.GetValue ("COREAPI_SCHEME", "http");
            this.Port = conf.GetValue ("COREAPI_PORT", "9110");
        }

        public CoreConfig(){}
    }
}