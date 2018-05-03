using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.ProcessMemoryClient;
using ONS.PlataformaSDK.Util;
using ONS.PlataformaSDK.Exception;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class ProcessApp
    {
        private string ProcessInstanceId;
        private string ProcessId;
        private ProcessMemoryHttpClient ProcessMemoryClient;
        private CoreClient CoreClient;
        public Context Context { get; set; }

        public ProcessApp(string processInstanceId, string processId, ProcessMemoryHttpClient processMemoryClient, CoreClient coreClient)
        {
            this.ProcessInstanceId = processInstanceId;
            this.ProcessId = processId;
            this.ProcessMemoryClient = processMemoryClient;
            this.CoreClient = coreClient;
            this.Context = new Context();
        }

        public async void Start()
        {
            var HeadTask = ProcessMemoryClient.Head(this.ProcessInstanceId);
            var ProcessMemory = await HeadTask;
            Context.Event = ProcessMemory.Event;

            var OperationTask = CoreClient.OperationByProcessIdAsync(ProcessId);
            var Operations = await OperationTask;
            VerifyOperationList(Operations);
        }

        public void VerifyOperationList(List<Operation> Operations)
        {
            if (Operations.isEmpty())
            {
                throw new PlataformaException($"Operation not found for process {this.ProcessId}");
            }

        }

    }
}
