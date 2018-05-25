using Newtonsoft.Json;
using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.EnvProps;
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
        }
        public ProcessMemoryHttpClient(HttpClient httpClient, EnvironmentProperties processMemoryEnvironmentProperties)
        {
            this.HttpClient = httpClient;
            this.ProcessMemoryEnvironmentProperties = processMemoryEnvironmentProperties;
        }
        public virtual ProcessMemoryEntity Head(string processInstanceId)
        {
            System.Console.WriteLine($"get head of process memory: {processInstanceId}");
            var ProcessMemoryTask = HttpClient.Get($"{ProcessMemoryEnvironmentProperties.Scheme}://{ProcessMemoryEnvironmentProperties.Host}:{ProcessMemoryEnvironmentProperties.Port}" +
                            $"/{processInstanceId}/head?app_origin=dotnet_sdk");
            var ProcessMemoryJson = ProcessMemoryTask.Result;
            var ProcessMemory = JsonConvert.DeserializeObject<ProcessMemoryEntity>(ProcessMemoryJson);
            return ProcessMemory;
        }

        public virtual void Commit(Context context)
        {
            System.Console.WriteLine("Commit context to process memory.");
            var JsonContent = JsonConvert.SerializeObject(context, Newtonsoft.Json.Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var url = $"{ProcessMemoryEnvironmentProperties.Scheme}://{ProcessMemoryEnvironmentProperties.Host}:{ProcessMemoryEnvironmentProperties.Port}" +
                            $"/{context.InstanceId}/commit?app_origin=dotnet_sdk";
            HttpClient.Post(url, JsonContent);
        }
    }
}
