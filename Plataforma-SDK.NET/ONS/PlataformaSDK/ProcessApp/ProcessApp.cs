using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.ProcessMemoryClient;
using ONS.PlataformaSDK.Util;
using ONS.PlataformaSDK.Exception;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class ProcessApp
    {
        public Context Context { get; set; }
        public string EventIn { get; set; }
        private string ProcessInstanceId;
        private string ProcessId;
        private ProcessMemoryHttpClient ProcessMemoryClient;
        private CoreClient CoreClient;

        public ProcessApp(string systemId, string processInstanceId, string processId, string eventIn, ProcessMemoryHttpClient processMemoryClient, CoreClient coreClient)
        {
            this.ProcessInstanceId = processInstanceId;
            this.ProcessId = processId;
            this.EventIn = eventIn;
            this.ProcessMemoryClient = processMemoryClient;
            this.CoreClient = coreClient;
            this.Context = new Context();
            //FIXME Construtor
            Context.InstanceId = this.ProcessInstanceId;
            Context.ProcessId = this.ProcessId;
            Context.SystemId = systemId;
        }

        public async Task Start()
        {
            var HeadTask = ProcessMemoryClient.Head(this.ProcessInstanceId);
            var ProcessMemory = await HeadTask;
            Context.Event = ProcessMemory.Event;

            var OperationTask = CoreClient.OperationByProcessIdAsync(ProcessId);
            var Operations = await OperationTask;
            VerifyOperationList(Operations);
            var Operation = Operations.Find(operation => operation.Event_In.Equals(this.EventIn));
            VerifyOperation(Operation);
            Context.EventOut = Operation.Event_Out;
            Context.Commit = Operation.Commit;

            this.StartProcess(Context);
        }

        private void StartProcess(Context context)
        {
        }

        public void VerifyOperationList(List<Operation> Operations)
        {
            if (Operations.isEmpty())
            {
                throw new PlataformaException($"Operation not found for process {this.ProcessId}");
            }

        }
        private void VerifyOperation(Operation operation)
        {
            if (operation == null)
            {
                throw new PlataformaException($"Operation not found for process {this.ProcessId}");
            }
        }

    }
}
