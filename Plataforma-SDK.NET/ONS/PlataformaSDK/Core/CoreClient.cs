using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Environment;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.Core
{
    public class CoreClient
    {
        private HttpClient HttpClient;
        private EnvironmentProperties ProcessMemoryEnvironmentProperties;

        public CoreClient()
        {
            //FIXME Interface
        }
        public CoreClient(HttpClient httpClient, EnvironmentProperties coreEnvironmentProperties)
        {
            this.HttpClient = httpClient;
            this.ProcessMemoryEnvironmentProperties = coreEnvironmentProperties;
        }
        public Operation OperationByProcessId() {
            return null;
        }
    }
}
