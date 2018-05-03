using Newtonsoft.Json;
using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Environment;
using ONS.PlataformaSDK.ProcessApp;
using ONS.PlataformaSDK.Entities;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.ProcessMemoryClient
{
    public class ProcessMemoryHttpClient
    {
        private HttpClient HttpClient;
        private EnvironmentProperties ProcessMemoryEnvironmentProperties;

        public ProcessMemoryHttpClient()
        {
            //FIXME Interface
        }
        public ProcessMemoryHttpClient(HttpClient httpClient, EnvironmentProperties processMemoryEnvironmentProperties)
        {
            this.HttpClient = httpClient;
            this.ProcessMemoryEnvironmentProperties = processMemoryEnvironmentProperties;
        }
        public virtual async Task<ProcessMemoryEntity> Head(string processInstanceId)
        {
            System.Console.WriteLine($"get head of process memory {processInstanceId}");
            var ProcessMemoryTask = HttpClient.Get($"{ProcessMemoryEnvironmentProperties.Scheme}://{ProcessMemoryEnvironmentProperties.Host}:{ProcessMemoryEnvironmentProperties.Port}" +
                            $"/{processInstanceId}/head");
            var ProcessMemoryJson = await ProcessMemoryTask;
            var ProcessMemory = JsonConvert.DeserializeObject<ProcessMemoryEntity>(ProcessMemoryJson);
            return ProcessMemory;
        }
    }
}
