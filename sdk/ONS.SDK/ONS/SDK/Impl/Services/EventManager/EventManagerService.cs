using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ONS.SDK.Configuration;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Services;
using ONS.SDK.Utils.Http;
using ONS.SDK.Worker;

namespace ONS.SDK.Impl.Services.EventManager 
{
    public class EventManagerService : IEventManagerService 
    {
        private static AgreededErroSendEvent[] _agreededErroSendEvent;

        static EventManagerService() {
            _agreededErroSendEvent = new AgreededErroSendEvent[]{
                new AgreededErroSendEvent { StatusCode = HttpStatusCode.BadRequest, RegexMessage = new Regex("has no subscribers")  },
                new AgreededErroSendEvent { StatusCode = HttpStatusCode.BadRequest, ErrorCode="empty_queue",  RegexMessage = new Regex("empty_queue")  }
            };
        }

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

            } catch(Exception ex) {
                
                SDKHttpException stex = null;
                AggregateException aex = ex as AggregateException;
                if (aex != null) {
                    stex = aex.InnerException as SDKHttpException;
                }

                ReponseSendEvent reponseSendEvent;
                if (_validateErrorSendEvent(stex, out reponseSendEvent)) {
                    this._logger.LogWarning($"There was an error sending the event[{e.Name}] to the manager in the instance execution. Response: {reponseSendEvent}");
                } else {
                    this._logger.LogError($"Error received from event manager is not allowed for correct system operation. Event[{e.Name}], Response: {reponseSendEvent}");
                    throw;
                }
            }
        }

        public void Save(MemoryEvent e) {
            Validate(e);
            this._client.Post<PushResponse>($"{this._config.Url}/save", e);
        }

        private bool _validateErrorSendEvent(SDKHttpException httpException, out ReponseSendEvent reponseSendEvent) {
            
            bool retorno = false;
            reponseSendEvent = null;

            if (httpException == null) return retorno;

            reponseSendEvent = !string.IsNullOrEmpty(httpException.ResponseBody)? 
                JsonConvert.DeserializeObject<ReponseSendEvent>(httpException.ResponseBody) : null;
            var statusCode = httpException.StatusCode;

            if (_agreededErroSendEvent != null && _agreededErroSendEvent.Length > 0) {
                foreach (var agreedeError in _agreededErroSendEvent)
                {
                    var eqStatusCode = agreedeError.StatusCode == statusCode;
                    
                    var eqErrorCode = string.IsNullOrEmpty(agreedeError.ErrorCode) || 
                        (reponseSendEvent != null && string.Equals(agreedeError.ErrorCode, reponseSendEvent.ErrorCode));
                    
                    var eqMsg = agreedeError.RegexMessage == null || 
                        (reponseSendEvent != null && !string.IsNullOrEmpty(reponseSendEvent.Message) && agreedeError.RegexMessage.Match(reponseSendEvent.Message).Success);

                    if (eqStatusCode && eqErrorCode && eqMsg) {
                        retorno = true;
                        break;
                    }
                }
            }
            
            return retorno;
        }
    }

    internal class AgreededErroSendEvent {
        
        public HttpStatusCode StatusCode {get;set;}
        
        public string ErrorCode {get;set;}

        public Regex RegexMessage {get;set;}
    }

    internal class ReponseSendEvent {
        
        [JsonProperty("error_code")]
        public string ErrorCode {get;set;}
        
        [JsonProperty("message")]
        public string Message {get;set;}

        public override string ToString() {
            return $"{this.GetType().Name}[ErrorCode={this.ErrorCode}, Message={this.Message}]";
        }
    }
}