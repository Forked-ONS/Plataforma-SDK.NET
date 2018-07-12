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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Worker
{
    public class WorkerEvent
    {
        private readonly ILogger<WorkerEvent> _logger;

        private readonly IEventManagerService _eventManagerService;

        private readonly IExecutionContext _executionContext;

        public WorkerEvent(ILogger<WorkerEvent> logger, 
            IExecutionContext executionContext, 
            IEventManagerService eventManagerService) 
        {
            this._logger = logger;
            this._executionContext = executionContext;
            this._eventManagerService = eventManagerService;
        }

        public void EmitEventError(Exception ex) {
            
            MemoryEvent eventError = null;

            if (this._executionContext != null && this._executionContext.ExecutionParameter != null &&
                this._executionContext.ExecutionParameter.MemoryEvent != null) 
            {
                var executionParameter = this._executionContext.ExecutionParameter;    

                var eventName = executionParameter.EventName;
                var lastIndex = eventName.IndexOf('.');
                if (lastIndex > 0) {
                
                    var eventNameError = eventName.Substring(0, lastIndex) + ".error";
                    eventError = new MemoryEvent() {
                        Name = eventNameError,
                        InstanceId = executionParameter.InstanceId,
                        Branch = executionParameter.Branch,
                        Reprocess = executionParameter.MemoryEvent.Reprocess,
                        Payload = new { message = ex.ToString() }
                    };   
                }
            }

            if (eventError != null) {
                this._eventManagerService.Push(eventError);
            } else {
                this._logger.LogError("System can not send error event because it has no instance event information.");
            }
        }

        public void EmitEventOut(IContext context) {
            
            var eventContext = context.GetEvent();

            MemoryEvent eventOut = new MemoryEvent() {
                Name = context.EventOut,
                Tag = eventContext.Tag,
                InstanceId = context.InstanceId,
                Branch = eventContext.Branch,
                Reprocess = eventContext.Reprocess,
                Payload = new { instanceId = context.InstanceId }
            };

            this._eventManagerService.Push(eventOut);
        }

        public void EmitEventPersistence(IContext context) {
            
            var executionParameter = this._executionContext.ExecutionParameter;
            var eventContext = context.GetEvent();

            MemoryEvent eventPersistence = new MemoryEvent() {
                Name = $"{context.SystemId}.persist.request",
                Tag = eventContext.Tag,
                InstanceId = context.InstanceId,
                Branch = eventContext.Branch,
                Reprocess = eventContext.Reprocess,
                ReferenceDate = eventContext.ReferenceDate,
                Payload = new { instanceId = context.InstanceId }
            };

            this._eventManagerService.Push(eventPersistence);
        }

    }
}