using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Environment;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.ProcessMemory
{
    public class ProcessMemoryClient
    {
        private HttpClient HttpClient;
        private EnvironmentProperties ProcessMemoryEnvironmentProperties;

        public ProcessMemoryClient()
        {
            //FIXME Interface
        }
        public ProcessMemoryClient(HttpClient httpClient, EnvironmentProperties processMemoryEnvironmentProperties)
        {
            this.HttpClient = httpClient;
            this.ProcessMemoryEnvironmentProperties = processMemoryEnvironmentProperties;
        }
        public Task<string> Head(string processInstanceId)
        {
            System.Console.WriteLine($"get head of process memory {processInstanceId}");
            return HttpClient.Get($"{ProcessMemoryEnvironmentProperties.Scheme}://{ProcessMemoryEnvironmentProperties.Host}:{ProcessMemoryEnvironmentProperties.Port}" +
                $"/{processInstanceId}/head");
        }
    }
}
