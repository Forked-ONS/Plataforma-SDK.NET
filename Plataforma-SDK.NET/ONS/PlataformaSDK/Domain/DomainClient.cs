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
    }
}