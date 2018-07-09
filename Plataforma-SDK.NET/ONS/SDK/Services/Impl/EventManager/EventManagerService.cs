using ONS.SDK.Configuration;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Utils.Http;
using ONS.SDK.Worker;

namespace ONS.SDK.Services.Impl.EventManager 
{
    public class EventManagerService : IEventManagerService 
    {
        private EventManagerConfig _config;
        
        private readonly JsonHttpClient _client;

        public EventManagerService (EventManagerConfig config, JsonHttpClient client) 
        {
            this._config = config;
            this._client = client;
        }

        private void Validate(MemoryEvent mevent) 
        {
            if(string.IsNullOrEmpty(mevent.Name)) {
                throw new SDKRuntimeException("Event name is required");
            }
            if(mevent.Payload == null){
                throw new SDKRuntimeException("Event payload is required");
            }
        }

        public void Push(MemoryEvent e) {
            Validate(e);
            this._client.Put<PushResponse>($"{this._config.Url}/sendevent", e);
        }

        public void Save(MemoryEvent e) {
            Validate(e);
            this._client.Post<PushResponse>($"{this._config.Url}/save", e);
        }
    }
}