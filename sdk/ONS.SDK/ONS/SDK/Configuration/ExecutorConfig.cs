using Microsoft.Extensions.Configuration;

namespace ONS.SDK.Configuration {

    /// <summary>
    /// Classe que representa as configurações de serviços do executor da plataforma.
    /// </summary>
    public class ExecutorConfig : BaseServiceConfig {
        public ExecutorConfig (IConfiguration conf) {
            this.Host = conf.GetValue ("EXECUTOR_HOST", "executor");
            this.Scheme = conf.GetValue ("EXECUTOR_SCHEME", "http");
            this.Port = conf.GetValue ("EXECUTOR_PORT", "8000");
        }
    }
}