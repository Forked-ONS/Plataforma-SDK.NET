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
using Plataforma_SDK.NET.ONS.SDK.Utils;
using Newtonsoft.Json;

namespace ONS.SDK.Worker
{
    public class SDKWorker
    {
        private readonly ILogger _logger;

        private readonly ContextBuilder _contextBuilder;

        private readonly IProcessMemoryService _processMemoryService;

        private readonly IDomainService _domainService;

        private readonly IExecutionContext _executionContext;

        public SDKWorker(ILogger<SDKWorker> logger, ContextBuilder contextBuilder, 
            IProcessMemoryService processMemoryService, IDomainService domainService, 
            IExecutionContext executionContext) 
        {
            this._logger = logger;
            this._contextBuilder = contextBuilder;
            this._processMemoryService = processMemoryService;
            this._domainService = domainService;
            this._executionContext = executionContext;
        }

        public void Run(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent) 
        {
            _run(new ContextBuilderParameters(payload, eventName));
        }

        public void Run()
        {
            _run(null);
        }

        public void _run(ContextBuilderParameters parameters) 
        {
            using(this._executionContext) {

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
                    throw;
                } catch(Exception ex) {
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
        
            var runner = GetRunner(methodInfo.DeclaringType);    

            if (runner == null) {
                throw new SDKRuntimeException(
                    string.Format("Type not register to dependence injection. Type={0}", methodInfo.DeclaringType));
            }

            if (context.DataContext.DataLoaded) {
                _processMemoryService.Commit(context.UpdateMemory());
            }

            var args = _resolveArgs(methodInfo, context);
            try {
                
                using(new SDKStopwatch(this._logger, 
                    "Execution of business method to respond to the event.", 
                    new LogValue("Event=", eventName), new LogValue("Method=", methodInfo.Name)))
                {
                    methodInfo.Invoke(runner, args);
                }

            } catch(Exception ex) {
                throw new SDKRuntimeException(
                    $"Error attempting to execute business method that responds to platform. Event={eventName}, Method={methodInfo.Name}", 
                    ex
                );
            }

            using(var watch = new SDKStopwatch(this._logger, "Save memory and dataset through SDK.",
                new LogValue("Event=", eventName), new LogValue("Method=", methodInfo.Name)))
            {
                _processMemoryService.Commit(context.UpdateMemory());

                if (context.Commit && !context.Memory.Event.IsReproduction) {
                    var trackingEntities = context.DataContext.TrackingEntities;
                    if (trackingEntities.Count > 0) {
                        this._domainService.Persist(context.Memory.Map.Name, trackingEntities);
                    }
                }    
            }
        }

        public object GetRunner(Type type) {
            if (SDKConfiguration.ServiceProvider == null) {
                throw new SDKRuntimeException("ServiceProvider not configurated in SDKConfiguration, verify configuration.");
            }
            return SDKConfiguration.ServiceProvider.GetService(type);
        }
        
        private object[] _resolveArgs(MethodInfo methodInfo, IContext context) 
        {    
            var retorno = new List<object>();
            var parameters = methodInfo.GetParameters();

            foreach(var parameter in parameters) {
                var type = parameter.ParameterType;

                if (typeof(IEvent).IsAssignableFrom(type)) {
                    retorno.Add(context.GetEvent());
                }
                else if (typeof(IPayload).IsAssignableFrom(type)) {
                    retorno.Add(context.GetEvent().GetPayload());
                }
                else if (typeof(IContext).IsAssignableFrom(type)) {
                    retorno.Add(context);
                }
                else if (typeof(IDataContext).IsAssignableFrom(type)) {
                    retorno.Add(context.DataContext);
                }
                else if (typeof(IDataSet).IsAssignableFrom(type)) {
                    
                    var methodSet = context.DataContext.GetType().GetMethods()
                        .FirstOrDefault(m => m.Name == "Set" && m.IsGenericMethod);
                    
                    IDataSet dataSet = null;
                    if (methodSet != null) {
                        var typeGeneric = type.GetGenericArguments();
                        methodSet = methodSet.MakeGenericMethod(typeGeneric);
                        dataSet = (IDataSet) methodSet.Invoke(context.DataContext, new object[0]);
                    }
                    retorno.Add(dataSet);
                }
            }

            return retorno.ToArray();
        }
    }
}