using ONS.SDK.Domain.Core;
using ONS.SDK.Domain.Services;
using ONS.SDK.Infra;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.EventManager {
    public class EventManagerService : IEventManagerService {
        private EventManagerConfig _config;
        private JsonHttpClient _client;

        public EventManagerService (EventManagerConfig config, JsonHttpClient client) {
            this._config = config;
            this._client = client;
        }

        public void Push<T> (Event<T> e) {
            this._client.Put<PushResponse>($"{this._config.Url}/sendevent", e);
        }
    }
}