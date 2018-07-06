using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Services.Impl.EventManager 
{
    public class EventManagerService : IEventManagerService 
    {
        private EventManagerConfig _config;
        
        private readonly JsonHttpClient _client;

        public EventManagerService (EventManagerConfig config, JsonHttpClient client) {
            this._config = config;
            this._client = client;
        }

        public void Push (MemoryEvent e) {
            this._client.Put<PushResponse>($"{this._config.Url}/sendevent", e);
        }
    }
}