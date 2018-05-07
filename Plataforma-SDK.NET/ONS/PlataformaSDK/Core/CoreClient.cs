using Newtonsoft.Json;
using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.Environment;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ONS.PlataformaSDK.Core
{
    public class CoreClient
    {
        private HttpClient HttpClient;
        private EnvironmentProperties CoreEnvironmentProperties;

        public CoreClient()
        {
            //FIXME Interface
        }
        public CoreClient(HttpClient httpClient, EnvironmentProperties coreEnvironmentProperties)
        {
            this.HttpClient = httpClient;
            this.CoreEnvironmentProperties = coreEnvironmentProperties;
        }
        public virtual async Task<List<Operation>> OperationByProcessIdAsync(string processId) {
            var OperationJsonTask = HttpClient.Get($"{CoreEnvironmentProperties.Scheme}://{CoreEnvironmentProperties.Host}:{CoreEnvironmentProperties.Port}" +
                $"/core/operation?filter=byProcessId&processId={processId}");
            var OperationJson = await OperationJsonTask;
            return JsonConvert.DeserializeObject<List<Operation>>(OperationJson);
        }

        public async Task<List<Map>> MapByProcessId(string processId) {
            System.Console.WriteLine($"get maps from api core for process id: {processId}");
            var MapJsonTask = HttpClient.Get($"{CoreEnvironmentProperties.Scheme}://{CoreEnvironmentProperties.Host}:{CoreEnvironmentProperties.Port}" +
                $"/core/map?filter=byProcessId&processId={processId}");
            var MapJson = await MapJsonTask;
            System.Console.WriteLine(MapJson);
            return JsonConvert.DeserializeObject<List<Map>>(MapJson);
        }
    }
}
