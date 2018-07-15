using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Microsoft.Extensions.Configuration;
using ONS.SDK.Configuration;
using ONS.SDK.Context;
using ONS.SDK.Data;
using ONS.SDK.Services;
using ONS.SDK.Services.Domain;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using ONS.SDK.Utils;
using Newtonsoft.Json;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Domain.Base;
using ONS.SDK.Domain.Core;
using ONS.SDK.Worker;

namespace ONS.SDK.Impl.Worker
{
    public class SDKWorker: ISDKWorker
    {
        private readonly ILogger _logger;

        private readonly IContextBuilder _contextBuilder;

        private readonly IExecutionContext _executionContext;

        private readonly WorkerEvent _workerEvent;

        private readonly IProcessMemoryService _processMemoryService;

        private readonly IDomainService _domainService;

        private readonly IProcessInstanceService _processInstanceService;

        private readonly IBranchService _branchService;

        public SDKWorker(ILogger<SDKWorker> logger, IContextBuilder contextBuilder, 
            IExecutionContext executionContext, 
            WorkerEvent workerEvent,
            IProcessMemoryService processMemoryService, 
            IDomainService domainService,
            IProcessInstanceService processInstanceService,
            IBranchService branchService) 
        {
            this._logger = logger;
            this._contextBuilder = contextBuilder;
            this._workerEvent = workerEvent;
            this._processMemoryService = processMemoryService;
            this._domainService = domainService;
            this._executionContext = executionContext;
            this._processInstanceService = processInstanceService;
            this._branchService = branchService;
        }

        public void Run(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent) 
        {
            _run(new ContextBuilderParameters(payload, eventName));
        }

        public void Run()
        {
            _run(null);
        }

        private void _run(ContextBuilderParameters parameters) 
        {
            using(this._executionContext.Begin()) {

                try {

                    using(var watch = new SDKStopwatch(this._logger, "Execution of event through SDK.",
                        new LogValue("Params=", parameters), new LogValue("ExecContext=", this._executionContext))) 
                    {

                        var context = _contextBuilder.Build(parameters);

                        watch.Log("Built the context to attend the event.");

                        if (this._logger.IsEnabled(LogLevel.Trace)) {

                            var objLogSerialized = JsonConvert.SerializeObject(context);
                            
                            this._logger.LogTrace($"Context built to execute method to attend event. " + 
                                $"Params={parameters} / ExecContext={this._executionContext} / Context={objLogSerialized}");
                        }

                        _runWithContext(context);
                    }
                
                } catch(SDKRuntimeException srex) {
                    this._logger.LogError($"Error execution of event through SDK. Params={parameters} / ExecContext={this._executionContext}", srex);
                    this._workerEvent.EmitEventError(srex);
                    throw;
                } catch(Exception ex) {
                    this._logger.LogError($"Error execution of event through SDK. Params={parameters} / ExecContext={this._executionContext}");
                    this._workerEvent.EmitEventError(ex);
                    throw new SDKRuntimeException("Platform SDKWorker execution error.", ex);
                }
            }
        }

        private void _runWithContext(IContext context) {

            if (context == null) {
                throw new SDKRuntimeException("Error building instance context.");
            }

            var eventName = context.GetEvent().Name;

            var methodInfo = SDKConfiguration.GetMethodByEvent(eventName);
            
            if (methodInfo == null) {
                
                throw new SDKRuntimeException(
                    string.Format("Method not found to event. Event={0}", eventName));
            }
        
            var runner = WorkerHelper.GetRunner(methodInfo.DeclaringType);    

            if (runner == null) {
                throw new SDKRuntimeException(
                    string.Format("Type not register to dependence injection. Type={0}", methodInfo.DeclaringType));
            }

            if (context.DataContext.DataLoaded) {
                _processMemoryService.Commit(context.UpdateMemory());
            }

            var args = WorkerHelper.ResolveArgs(methodInfo, context);
            try {
                
                using(new SDKStopwatch(this._logger, 
                    "Execution of business method to respond to the event.", 
                    new LogValue("Event=", eventName), new LogValue("Method=", methodInfo.Name), 
                    new LogValue("InstanceId=", context.InstanceId)))
                {
                    methodInfo.Invoke(runner, args);
                }

            } catch(Exception ex) {
                
                var sdkExcpt = ex.InnerException as SDKRuntimeException;
                if (sdkExcpt != null) {
                    throw sdkExcpt;
                } else {
                    throw new SDKRuntimeException(
                        $"Error attempting to execute business method that responds to platform. " + 
                        $"Event={eventName}, Method={methodInfo.Name}, InstanceId={context.InstanceId}", 
                        ex.InnerException
                    );
                }
            }

            using(new SDKStopwatch(this._logger, "Save memory and dataset through SDK.",
                new LogValue("Event=", eventName), new LogValue("Method=", methodInfo.Name),
                new LogValue("InstanceId=", context.InstanceId)))
            {
                 _finishExecution(context);
            }
        }

        private void _finishExecution(IContext context) 
        {
            var eventName = context.GetEvent().Name;    

            if (context.Fork != null) 
            {
                this._fork(context);
            }
            
            _processMemoryService.Commit(context.UpdateMemory());

            if (context.Commit 
                && !context.Memory.Event.IsReproduction) 
            {
                if (this._executionContext.ExecutionParameter.SynchronousPersistence) 
                {
                    var trackingEntities = context.DataContext.TrackingEntities;
                    
                    if (trackingEntities.Count > 0) {
                        this._domainService.Persist(context.Memory.Map.Name, trackingEntities);
                    }
                    this._workerEvent.EmitEventOut(context);
                } else {
                    this._workerEvent.EmitEventPersistence(context);
                }
            }    
        }

        private void _fork(IContext context) 
        {
            var fork = context.Fork;
            var entities = context.DataContext.AllEntities;
            var startedAt = DateTime.Now;

            if (entities != null && entities.Any()) {
                foreach (var entity in entities)
                {
                    var current = entity._Metadata.ModifiedAt;
                    if (current.HasValue && current.Value < startedAt) {
                        startedAt = current.Value;
                    }

                    entity._Metadata.Branch = fork.Name;
                }
            }
            
            fork.StartedAt = startedAt;
            
            var processInstance = new ProcessInstance() { Id = context.InstanceId, IsFork = true };
            
            this._processInstanceService.Save(processInstance);

            var branches = this._branchService.FindBySystemIdAndName(context.SystemId, fork.Name);
            if (branches == null || !branches.Any()) {
                this._branchService.Save(fork);
            }
            else {
                throw new SDKRuntimeException($"Fork already exist. Fork.Name={fork.Name}, InstanceId={context.InstanceId}");
            }
        }
        
    }
}