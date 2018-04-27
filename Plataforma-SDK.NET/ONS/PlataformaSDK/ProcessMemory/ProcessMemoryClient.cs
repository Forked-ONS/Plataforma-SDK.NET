using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Environment;

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
        public string Head(string processInstanceId)
        {
            System.Console.WriteLine($"{ProcessMemoryEnvironmentProperties.Scheme}://{ProcessMemoryEnvironmentProperties.Host}:{ProcessMemoryEnvironmentProperties.Port}" +
                $"/{processInstanceId}/head");
            System.Console.WriteLine("--------------");
            return HttpClient.Get($"{ProcessMemoryEnvironmentProperties.Scheme}://{ProcessMemoryEnvironmentProperties.Host}:{ProcessMemoryEnvironmentProperties.Port}" +
                $"/{processInstanceId}/head");
        }
    }
}
