using Microsoft.Extensions.Configuration;

namespace ONS.SDK.Infra {
    public class ProcessMemoryConfig : BaseServiceConfig {
        public ProcessMemoryConfig (IConfiguration conf) {
            this.Host = conf.GetValue ("PROCESS_MEMORY_HOST", "process_memory");
            this.Scheme = conf.GetValue ("PROCESS_MEMORY_SCHEME", "http");
            this.Port = conf.GetValue ("PROCESS_MEMORY_PORT", "9091");
        }

        public ProcessMemoryConfig() {}
    }
}