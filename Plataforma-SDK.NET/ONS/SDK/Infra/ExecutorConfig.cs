using Microsoft.Extensions.Configuration;

namespace ONS.SDK.Infra {
    public class ExecutorConfig : BaseServiceConfig {
        public ExecutorConfig (IConfiguration conf) {
            this.Host = conf.GetValue ("EXECUTOR_HOST", "executor");
            this.Scheme = conf.GetValue ("EXECUTOR_SCHEME", "http");
            this.Port = conf.GetValue ("EXECUTOR_PORT", "8000");
        }
    }
}