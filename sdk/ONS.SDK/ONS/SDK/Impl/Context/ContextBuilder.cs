/**
Context builder é a classe responsável por montar um objeto de contexto de execução
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONS.SDK.Configuration;
using ONS.SDK.Context;
using ONS.SDK.Data;
using ONS.SDK.Data.Persistence;
using ONS.SDK.Domain.Core;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Services;
using ONS.SDK.Utils;
using ONS.SDK.Worker;
using YamlDotNet.Serialization;

namespace ONS.SDK.Impl.Context {

    public class ContextBuilder : IContextBuilder
    {

        private readonly IProcessMemoryService _processMemory;

        private readonly IDataContextBuilder _dataContextBuilder;

        private readonly IExecutorService _executorService;

        private readonly IEventManagerService _eventManagerService;

        private readonly IOperationService _operationService;

        private readonly IMapService _mapService;

        private readonly IExecutionContext _executionContext;

        private readonly ILogger<ContextBuilder> _logger;

        public ContextBuilder(ILogger<ContextBuilder> logger, IProcessMemoryService processMemory, 
            IDataContextBuilder dataContextBuilder, IExecutorService executorService,
            IEventManagerService eventManagerService, IOperationService operationService, 
            IExecutionContext executionContext, IMapService mapService) 
        {
            this._logger = logger;
            this._processMemory = processMemory;
            this._dataContextBuilder = dataContextBuilder;
            this._executorService = executorService;
            this._eventManagerService = eventManagerService;
            this._operationService = operationService;
            this._executionContext = executionContext;
            this._mapService = mapService;
        }

        public IContext Build(ContextBuilderParameters parameters = null) {
            
            return parameters != null ? Build(parameters.Payload, parameters.EventName) : Build();
        }

        public IContext Build() {

            if (this._logger.IsEnabled(LogLevel.Debug)) {
                this._logger.LogDebug($"Building context from ExecutionContext={this._executionContext}");
            }

            if (string.IsNullOrEmpty(this._executionContext.ProcessId)) {
                throw new SDKRuntimeException("Process ID of process not found. ENV: PROCESS_ID");
            }
            if (string.IsNullOrEmpty(this._executionContext.ProcessInstanceId)) {
                throw new SDKRuntimeException("Instance ID of process not found. ENV: INSTANCE_ID");
            }

            var memory = _recoveryMemory(this._executionContext.ProcessId, this._executionContext.ProcessInstanceId);

            return _buildContextFromMemory(memory);
        }
        
        public IContext Build(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent) 
        {
            if (this._logger.IsEnabled(LogLevel.Debug)) {
                this._logger.LogDebug($"Building context from Payload={payload} and Event={eventName}");
            }

            if (payload == null) {
                payload = new EmptyPayload();
            }

            _executionContext.ExecutionParameter.SynchronousPersistence = true;

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
            //return null;
        }

        private Memory _recoveryMemory(string processId, string instanceId) 
        {   
            if (this._logger.IsEnabled(LogLevel.Debug)) {
                this._logger.LogDebug($"Recovery memory to building context. ProcessId={processId} and InstanceId={instanceId}");
            }

            var memory = _processMemory.Head(instanceId);
            if (memory == null) {
                throw new SDKRuntimeException($"Process Memory instance was not found. processId: {processId}, instanceId: {instanceId}");
            }

            if (this._logger.IsEnabled(LogLevel.Trace)) {
                this._logger.LogTrace($"Memory recovered from ProcessMemory. processId: {processId}, instanceId: {instanceId}.");
            }

            if (memory.Event.IsReproduction) {
                memory.Event.Scope = SDKConstants.Reproduction;
            }

            _executionContext.ExecutionParameter.Memory = memory;
            _executionContext.ExecutionParameter.InstanceId = instanceId;

            var operations = _operationService.FindByProcessId(processId);

            Operation operation = operation = operations != null ? 
                operations.FirstOrDefault(it => string.Equals(it.Event_In, memory.Event.Name)) : null;  

            if (operation == null) {
                throw new SDKRuntimeException($"Operation not found for process {processId}");
            }
            
            memory.ProcessId = processId;
            memory.SystemId = operation.SystemId;
            memory.InstanceId = instanceId;
            memory.EventOut = operation.Event_Out;
            memory.Commit = operation.Commit;

            if (memory.Map == null) {
                
                var maps = this._mapService.FindByProcessId(processId);
                
                // TODO map para desenvolvimento: var map = maps.FirstOrDefault();
                var map = maps.LastOrDefault();
                if (map != null) 
                {    
                    memory.Map = ProcessMap.ConvertFromMap(map);
                }
            }

            return memory;
        }
        
        private IContext _buildContextFromMemory(Memory memory) 
        {    
            if (this._logger.IsEnabled(LogLevel.Debug)) {
                this._logger.LogDebug($"Building context from memory. InstanceId={memory.InstanceId}");
            }

            var eventName = memory.Event.Name;

            var typePayload = SDKConfiguration.GetTypePayload(eventName);
            if (typePayload == null) {
                throw new BadConfigException($"Type of payload from context event not configurated. InstanceId={memory.InstanceId}");
            }

            var typeContext = typeof(SDKContext<>).MakeGenericType(typePayload);
            
            IDataContext dataContext;
            using(new SDKStopwatch(this._logger, 
                "Execution of dataContext construction through SDK.", 
                new LogValue("InstanceId=", memory.InstanceId))) 
            {
                dataContext = this._dataContextBuilder.Build(memory);
            }

            var context = (IContext) Activator.CreateInstance(typeContext, memory, dataContext);

            JObject jobjPayload = context.GetEvent().MemoryEvent.Payload as JObject;
            if (jobjPayload != null) {
                context.GetEvent().SetPayload((IPayload) jobjPayload.ToObject(typePayload));
            }

            return context;
        }
        
    }

    
}