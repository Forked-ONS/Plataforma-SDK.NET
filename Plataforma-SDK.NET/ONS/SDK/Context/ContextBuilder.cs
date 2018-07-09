/**
Context builder é a classe responsável por montar um objeto de contexto de execução
*/
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONS.SDK.Configuration;
using ONS.SDK.Data;
using ONS.SDK.Domain.Core;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Services;
using ONS.SDK.Worker;

namespace ONS.SDK.Context {

    public class ContextBuilder {

        private IProcessMemoryService _processMemory;

        private IDataContextBuilder _dataContextBuilder;

        private IExecutorService _executorService;

        private IEventManagerService _eventManagerService;

        private IOperationService _operationService;

        public ContextBuilder () { }

        public ContextBuilder(IProcessMemoryService processMemory, 
            IDataContextBuilder dataContextBuilder, IExecutorService executorService,
            IEventManagerService eventManagerService, IOperationService operationService) 
        {
            this._processMemory = processMemory;
            this._dataContextBuilder = dataContextBuilder;
            this._executorService = executorService;
            this._eventManagerService = eventManagerService;
            this._operationService = operationService;
        }

        public IContext Build(string instanceId) {

            // TODO colocar logs
            var memory = _processMemory.Head(instanceId);

            var eventName = memory.Event.Name;

            var typePayload = SDKConfiguration.GetTypePayload(eventName);

            var typeContext = typeof(SDKContext<>).MakeGenericType(typePayload);
            
            var dataContext = this._dataContextBuilder.Build(memory.DataSet);

            var context = (IContext) Activator.CreateInstance(typeContext, memory, dataContext);

            JObject jobjPayload = context.GetEvent().MemoryEvent.Payload as JObject;
            if (jobjPayload != null) {
                context.GetEvent().SetPayload((IPayload) jobjPayload.ToObject(typePayload));
            }

            return context;
        }

        public IContext Build(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent) {

            var typePayload = payload.GetType();

            var memoryEvent = new MemoryEvent();

            memoryEvent.Payload = payload;
            memoryEvent.Name = eventName;

            var processInstance = this._executorService.CreateInstance(memoryEvent);

            _eventManagerService.Save(memoryEvent);

            var instanceId = processInstance.Id;

            var memory = _processMemory.Head(instanceId);
            if (memory == null) {
                // TODO Adicionar na exceção dados da transação.
                throw new SDKRuntimeException("Process Memory instance was not found.");
            }

            if (memory.Event.IsReproduction) {
                // TODO colocar em constante
                memory.Event.Scope = "reproduction";
            }

            // TODO falata data de referência??
            var operations = _operationService.FindByProcessId(processInstance.ProcessId);

            Operation operation = null;
            if (operations != null) {
                operation = operations.FirstOrDefault(it => string.Equals(it.Event_In, eventName));  
            } 

            if (operation == null) {
                throw new SDKRuntimeException($"Operation not found for process {processInstance.ProcessId}");
            }
            
            // log("Creating context");
            memory.ProcessId = processInstance.ProcessId;
            memory.SystemId = processInstance.SystemId;
            memory.InstanceId = instanceId;
            memory.EventOut = operation.Event_Out;
            memory.Commit = operation.Commit;
            


            //Console.WriteLine("############## instanceId: " + instanceId);
            Console.WriteLine("############## memory: " + JsonConvert.SerializeObject(memory));
            
            return Build(instanceId);
        }
        
    }
}