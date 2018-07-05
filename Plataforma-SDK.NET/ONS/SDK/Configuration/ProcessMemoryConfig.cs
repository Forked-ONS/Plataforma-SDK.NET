using Microsoft.Extensions.Configuration;

namespace ONS.SDK.Configuration {
    public class ProcessMemoryConfig : BaseServiceConfig {
        public ProcessMemoryConfig (IConfiguration conf) {
            this.Host = conf.GetValue ("PROCESS_MEMORY_HOST", "localhost");
            this.Scheme = conf.GetValue ("PROCESS_MEMORY_SCHEME", "http");
            this.Port = conf.GetValue ("PROCESS_MEMORY_PORT", "9091");
        }

        public ProcessMemoryConfig() {}
    }
}