using System;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.Environment;
using ONS.PlataformaSDK.Http;

namespace ONS.PlataformaSDK.EventManager
{
    public class EventManagerClient
    {
        private HttpClient HttpClient;
        private EnvironmentProperties EventManagerEnvironmentProperties;

        public EventManagerClient()
        {
        }

        public EventManagerClient(HttpClient httpClient, EnvironmentProperties eventManagerEnvironmentProperties)
        {
            this.HttpClient = httpClient;
            this.EventManagerEnvironmentProperties = eventManagerEnvironmentProperties;
        }

        public virtual void SendEvent(Event pEvent)
        {
            
        }
    }
}