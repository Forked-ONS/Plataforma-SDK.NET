/**
Context builder é a classe responsável por montar um objeto de contexto de execução
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONS.SDK.Configuration;
using ONS.SDK.Data;
using ONS.SDK.Domain.Core;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Services;
using ONS.SDK.Worker;
using YamlDotNet.Serialization;

namespace ONS.SDK.Context {

    public class ContextBuilder {

        private IProcessMemoryService _processMemory;

        private IDataContextBuilder _dataContextBuilder;

        private IExecutorService _executorService;

        private IEventManagerService _eventManagerService;

        private IOperationService _operationService;

        private IMapService _mapService;

        private IExecutionContext _executionContext;

        public ContextBuilder () { }

        public ContextBuilder(IProcessMemoryService processMemory, 
            IDataContextBuilder dataContextBuilder, IExecutorService executorService,
            IEventManagerService eventManagerService, IOperationService operationService, 
            IExecutionContext executionContext, IMapService mapService) 
        {
            this._processMemory = processMemory;
            this._dataContextBuilder = dataContextBuilder;
            this._executorService = executorService;
            this._eventManagerService = eventManagerService;
            this._operationService = operationService;
            this._executionContext = executionContext;
            this._mapService = mapService;
        }

        public IContext Build() {

            if (string.IsNullOrEmpty(_executionContext.ProcessId)) {
                throw new SDKRuntimeException("Process ID of process not found. ENV: PROCESS_ID");
            }
            if (string.IsNullOrEmpty(_executionContext.ProcessInstanceId)) {
                throw new SDKRuntimeException("Instance ID of process not found. ENV: INSTANCE_ID");
            }

            var memory = _recoveryMemory(_executionContext.ProcessId, _executionContext.ProcessInstanceId);

            return _buildContextFromMemory(memory);
        }
        
        public IContext Build(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent) 
        {
            if (payload == null) {
                payload = new EmptyPayload();
            }

            var typePayload = payload.GetType();

            var memoryEvent = new MemoryEvent();

            memoryEvent.Payload = payload;
            memoryEvent.Name = eventName;
            memoryEvent.AppOrigin = SDKConfiguration.AppOriginSDK;

            var processInstance = this._executorService.CreateInstance(memoryEvent);

            _eventManagerService.Save(memoryEvent);

            var processId = processInstance.ProcessId;
            var instanceId = processInstance.Id;
            
            var memory = _recoveryMemory(processId, instanceId);

            return _buildContextFromMemory(memory);
        }

        private Memory _recoveryMemory(string processId, string instanceId) 
        {    
            var memory = _processMemory.Head(instanceId);
            if (memory == null) {
                throw new SDKRuntimeException($"Process Memory instance was not found. processId: {processId}, instanceId: {instanceId}");
            }

            if (memory.Event.IsReproduction) {
                memory.Event.Scope = SDKConstants.Reproduction;
            }

            _executionContext.ExecutionParameter = new ExecutionParameter() {
                Branch = memory.Event.Branch,
                ReferenceDate = memory.Event.ReferenceDate.HasValue ? memory.Event.ReferenceDate.Value : DateTime.Now,
                InstanceId = instanceId
            };

            // TODO falta data de referência??
            var operations = _operationService.FindByProcessId(processId);

            Operation operation = operation = operations != null ? 
                operations.FirstOrDefault(it => string.Equals(it.Event_In, memory.Event.Name)) : null;  

            if (operation == null) {
                throw new SDKRuntimeException($"Operation not found for process {processId}");
            }
            
            memory.ProcessId = operation.ProcessId;
            memory.SystemId = operation.SystemId;
            memory.InstanceId = instanceId;
            memory.EventOut = operation.Event_Out;
            memory.Commit = operation.Commit;

            if (memory.Map == null) {
                var maps = this._mapService.FindByProcessId(operation.ProcessId);
                var map = maps.FirstOrDefault();
                if (map != null) {
                    Console.WriteLine("################### operations: " + JsonConvert.SerializeObject(map));
                    memory.Map = ProcessMap.ConvertFromMap(map);
                }
            }

            return memory;
        }
        private IContext _buildContextFromMemory(Memory memory) {
            
            var eventName = memory.Event.Name;

            var typePayload = SDKConfiguration.GetTypePayload(eventName);

            var typeContext = typeof(SDKContext<>).MakeGenericType(typePayload);
            
            Console.WriteLine("################### memory: " + JsonConvert.SerializeObject(memory));

            var dataContext = this._dataContextBuilder.Build(memory);

            var context = (IContext) Activator.CreateInstance(typeContext, memory, dataContext);

            JObject jobjPayload = context.GetEvent().MemoryEvent.Payload as JObject;
            if (jobjPayload != null) {
                context.GetEvent().SetPayload((IPayload) jobjPayload.ToObject(typePayload));
            }

            return context;
        }
        
    }
}