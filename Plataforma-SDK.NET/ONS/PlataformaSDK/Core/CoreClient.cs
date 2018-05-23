using Newtonsoft.Json;
using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.Environment;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ONS.PlataformaSDK.Core
{
    public class CoreClient
    {
        private HttpClient HttpClient;
        private EnvironmentProperties CoreEnvironmentProperties;

        public CoreClient()
        {
        }

        public CoreClient(HttpClient httpClient, EnvironmentProperties coreEnvironmentProperties)
        {
            this.HttpClient = httpClient;
            this.CoreEnvironmentProperties = coreEnvironmentProperties;
        }
        public virtual async Task<List<Operation>> OperationByProcessIdAsync(string processId)
        {
            var OperationJsonTask = HttpClient.Get($"{CoreEnvironmentProperties.Scheme}://{CoreEnvironmentProperties.Host}:{CoreEnvironmentProperties.Port}" +
                $"/core/operation?filter=byProcessId&processId={processId}");
            var OperationJson = await OperationJsonTask;
            return JsonConvert.DeserializeObject<List<Operation>>(OperationJson);
        }

        public virtual async Task<List<PlatformMap>> MapByProcessId(string processId)
        {
            System.Console.WriteLine($"get maps from api core for process id: {processId}");
            var MapJsonTask = HttpClient.Get($"{CoreEnvironmentProperties.Scheme}://{CoreEnvironmentProperties.Host}:{CoreEnvironmentProperties.Port}" +
                $"/core/map?filter=byProcessId&processId={processId}");
            var MapJson = await MapJsonTask;
            return JsonConvert.DeserializeObject<List<PlatformMap>>(MapJson);
        }

        public virtual async Task<List<InstalledApp>> FindInstalledAppBySystemIdAndType(string systemId, string applicationType)
        {
            System.Console.WriteLine($"Get installed app by System Id ${systemId} and aplication type ${applicationType}");
            var AppJsonTask = HttpClient.Get($"{CoreEnvironmentProperties.Scheme}://{CoreEnvironmentProperties.Host}:{CoreEnvironmentProperties.Port}" +
                $"/core/installedApp?filter=bySystemIdAndType&systemId={systemId}&type={applicationType}");
            var AppJson = await AppJsonTask;
            return JsonConvert.DeserializeObject<List<InstalledApp>>(AppJson);
        }
    }
}
