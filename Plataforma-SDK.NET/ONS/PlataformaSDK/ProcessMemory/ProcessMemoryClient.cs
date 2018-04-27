using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Environment;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.ProcessMemory
{
    public class ProcessMemoryClient
    {
        private HttpClient HttpClient;
        private EnvironmentProperties ProcessMemoryEnvironmentProperties;
        public ProcessMemoryClient(HttpClient httpClient, EnvironmentProperties processMemoryEnvironmentProperties)
        {
            this.HttpClient = httpClient;
            this.ProcessMemoryEnvironmentProperties = processMemoryEnvironmentProperties;
        }
        public Task<string> Head(string processInstanceId)
        {
            return HttpClient.Get($"{ProcessMemoryEnvironmentProperties.Scheme}://{ProcessMemoryEnvironmentProperties.Host}:{ProcessMemoryEnvironmentProperties.Port}" +
                $"/{processInstanceId}/head");
        }
    }
}
