using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<EventManagerService> _logger;

        public EventManagerService(ILogger<EventManagerService> logger, EventManagerConfig config, JsonHttpClient client) 
        {
            this._config = config;
            this._client = client;
            this._logger = logger;
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
            try {
                this._client.Put<PushResponse>($"{this._config.Url}/sendevent", e);
            } catch(SDKHttpException stex) {
                if (stex.StatusCode == HttpStatusCode.BadRequest && 
                    !string.IsNullOrEmpty(stex.ResponseBody) && stex.ResponseBody.IndexOf("has no subscribers") > 0) 
                {
                    this._logger.LogWarning($"Event {e.Name} has no subscribers.");
                } else {
                    throw;
                }
            }
        }

        public void Save(MemoryEvent e) {
            Validate(e);
            this._client.Post<PushResponse>($"{this._config.Url}/save", e);
        }
    }
}