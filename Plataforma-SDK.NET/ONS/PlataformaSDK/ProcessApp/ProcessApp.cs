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
using ONS.PlataformaSDK.Domain;
using ONS.PlataformaSDK.EventManager;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class ProcessAppImpl
    {
        public Context Context { get; set; }
        public string EventIn { get; set; }
        public DataSetBuilder DataSetBuilder { get; set; }
        public IExecutable App { get; set; }
        private string ProcessInstanceId;
        private string ProcessId;
        private ProcessMemoryHttpClient ProcessMemoryClient;
        private CoreClient CoreClient;
        private EventManagerClient EventManagerClient;
        private bool SyncDomain;

        public ProcessAppImpl(string systemId, string processInstanceId, string processId, string eventIn, IDomainContext domainContext,
            ProcessMemoryHttpClient processMemoryClient, CoreClient coreClient, DomainClient domainClient, EventManagerClient eventManagerClient)
        {
            this.ProcessInstanceId = processInstanceId;
            this.ProcessId = processId;
            this.EventIn = eventIn;
            this.ProcessMemoryClient = processMemoryClient;
            this.CoreClient = coreClient;
            this.EventManagerClient = eventManagerClient;
            this.Context = new Context();
            Context.InstanceId = this.ProcessInstanceId;
            Context.ProcessId = this.ProcessId;
            Context.SystemId = systemId;
            this.DataSetBuilder = new DataSetBuilder(domainContext, domainClient);
        }

        public ProcessAppImpl(string systemId, string processInstanceId, string processId, string eventIn, bool syncDomain, IDomainContext domainContext,
            ProcessMemoryHttpClient processMemoryClient, CoreClient coreClient, DomainClient domainClient, EventManagerClient eventManagerClient)
                : this(systemId, processInstanceId, processId, eventIn, domainContext, processMemoryClient, coreClient, domainClient, eventManagerClient)
        {
            this.SyncDomain = syncDomain;
        }

        public void Start()
        {
            var ProcessMemory = ProcessMemoryClient.Head(this.ProcessInstanceId);
            Context.Event = ProcessMemory.Event;

            var Operations = CoreClient.OperationByProcessId(ProcessId);
            VerifyOperationList(Operations);
            var Operation = Operations.Find(operation => operation.Event_In.Equals(this.EventIn));
            VerifyOperation(Operation);
            Context.EventOut = Operation.Event_Out;
            Context.Commit = Operation.Commit;

            this.StartProcess();
        }

        public void StartProcess()
        {
            var PlatformsMaps = CoreClient.MapByProcessId(this.ProcessId);
            if (!PlatformsMaps.isEmpty())
            {
                Context.Map = PlatformsMaps[0];
                DataSetBuilder.Build(PlatformsMaps[0], Context.Event.Payload);
            }
            App.Execute(DataSetBuilder.DomainContext, Context.Event.Payload);
            Context.Entities = DataSetBuilder.DomainContext;
            ProcessMemoryClient.Commit(Context);
            PersistDomain();
        }

        public void PersistDomain()
        {
            if (Context.Commit && !IsReproduction() && !this.SyncDomain)
            {
                System.Console.WriteLine("Send event to persist");
                var Event = new Event();
                Event.Name = Context.SystemId + ".persist.request";
                Event.Instance_Id = Context.InstanceId;
                Event.Payload = new { instanceId = Context.InstanceId };
                EventManagerClient.SendEvent(Event);
            }
            else if (Context.Commit && this.SyncDomain)
            {
                System.Console.WriteLine("commiting data to domain synchronously");
                DataSetBuilder.Persist();
            }
            else
            {
                System.Console.WriteLine("Event's origin is a reproduction skip to save domain");
            }
        }

        private bool IsReproduction()
        {
            return false;
        }

        public void VerifyOperationList(List<Operation> operations)
        {
            if (operations.isEmpty())
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
