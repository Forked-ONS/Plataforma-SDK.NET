using Microsoft.Extensions.Configuration;

namespace ONS.SDK.Configuration {
    public class EventManagerConfig : BaseServiceConfig {
        public EventManagerConfig (IConfiguration conf) {
            this.Host = conf.GetValue ("EVENT_MANAGER_HOST", "event_manager");
            this.Scheme = conf.GetValue ("EVENT_MANAGER_SCHEME", "http");
            this.Port = conf.GetValue ("EVENT_MANAGER_PORT", "8081");
        }

        public EventManagerConfig() {}
    }
}