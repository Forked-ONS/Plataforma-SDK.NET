using Newtonsoft.Json.Linq;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.ProcessMemory;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class ProcessApp
    {
        private string ProcessInstanceId;
        private ProcessMemoryClient ProcessMemoryClient;
        private CoreClient CoreClient;
        public Context Context{get; set;}

        public ProcessApp(string processInstanceId, ProcessMemoryClient processMemoryClient, CoreClient coreClient)
        { 
            this.ProcessInstanceId = processInstanceId;
            this.ProcessMemoryClient = processMemoryClient;
            this.CoreClient = coreClient; 
            this.Context = new Context();  
        }

        public async void Start()
        {
            Task<string> HeadTask = ProcessMemoryClient.Head(this.ProcessInstanceId);
            var head = await HeadTask;
        }

    }
}
