using ONS.PlataformaSDK.Http;

namespace ONS.PlataformaSDK.ProcessMemory
{
    public class ProcessMemoryClient
    {
        private HttpClient HttpClient;
        public ProcessMemoryClient(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }
        public string Head(string processInstanceId)
        {

            return HttpClient.get("");
        }
    }
}
