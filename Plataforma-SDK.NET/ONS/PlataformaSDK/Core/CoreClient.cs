using Newtonsoft.Json;
using ONS.PlataformaSDK.Http;
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
        public async Task<List<Operation>> OperationByProcessIdAsync(string processId) {
            var OperationJsonTask = HttpClient.Get($"{CoreEnvironmentProperties.Scheme}://{CoreEnvironmentProperties.Host}:{CoreEnvironmentProperties.Port}" +
                $"/core/operation?filter=byProcessId&processId={processId}");
            var OperationJson = await OperationJsonTask;
            var Operations = JsonConvert.DeserializeObject<List<Operation>>(OperationJson);
            return Operations;
        }
    }
}
