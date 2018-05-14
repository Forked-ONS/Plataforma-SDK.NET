using System.Collections.Generic;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.Environment;
using ONS.PlataformaSDK.Http;

namespace ONS.PlataformaSDK.Domain
{
    public class DomainClient
    {
        private HttpClient HttpClient;
        private EnvironmentProperties CoreEnvironmentProperties;

        public DomainClient()
        {
        }
        
        public DomainClient(HttpClient httpClient, EnvironmentProperties coreEnvironmentProperties)
        {
            this.HttpClient = httpClient;
            this.CoreEnvironmentProperties = coreEnvironmentProperties;
        }

        public virtual void FindByFilter(string processName, Filter filter)
        {
        }
    }
}