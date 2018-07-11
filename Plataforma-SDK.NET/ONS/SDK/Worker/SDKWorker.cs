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

namespace ONS.SDK.Worker
{
    public class SDKWorker
    {
        private readonly ContextBuilder _contextBuilder;

        private readonly IProcessMemoryService _processMemoryService;

        private readonly IDomainService _domainService;

        private readonly IExecutionContext _executionContext;

        public SDKWorker(ContextBuilder contextBuilder, 
            IProcessMemoryService processMemoryService, IDomainService domainService, 
            IExecutionContext executionContext) 
        {
            _contextBuilder = contextBuilder;
            _processMemoryService = processMemoryService;
            _domainService = domainService;
            _executionContext = executionContext;
        }

        public object GetRunner(Type type) {
            // TODO validar se existe service provider, emitir erro com UseServiceProvider 
            // para ser chamado
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

        public void Run(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent) 
        {
            using(this._executionContext) {

                try {
                    var context = _contextBuilder.Build(payload, eventName);

                    _run(context);
                
                } catch(SDKRuntimeException srex) {
                    throw;
                } catch(Exception ex) {
                    throw new SDKRuntimeException("Platform SDKWorker execution error.", ex);
                }
            }
        }

        public void Run()
        {
            using(this._executionContext) {

                try {
                    var context = _contextBuilder.Build();

                    _run(context);
                
                } catch(SDKRuntimeException srex) {
                    throw;
                } catch(Exception ex) {
                    throw new SDKRuntimeException("Platform SDKWorker execution error.", ex);
                }
            }
        }

        private void _run(IContext context) {

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
                methodInfo.Invoke(runner, args);
            } catch(Exception ex) {
                // TODO verificar se vai logar mais detalhes.
                throw new SDKRuntimeException(
                    $"Error attempting to execute business method that responds to platform event. method: {methodInfo.Name}", 
                    ex
                );
            }

            _processMemoryService.Commit(context.UpdateMemory());

            if (context.Commit && !context.Memory.Event.IsReproduction) {
                var trackingEntities = context.DataContext.TrackingEntities;
                if (trackingEntities.Count > 0) {
                    this._domainService.Persist(context.Memory.Map.Name, trackingEntities);
                }
            }
        }
    }
}