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

namespace ONS.SDK.Worker
{
    public class SDKWorker
    {
        private readonly ContextBuilder _contextBuilder;

        private readonly IProcessMemoryService _processMemoryService;

        public SDKWorker(ContextBuilder contextBuilder, IProcessMemoryService processMemoryService) {
            _contextBuilder = contextBuilder;
            _processMemoryService = processMemoryService;
        }

        public object GetRunner(Type type) {
            // TODO validar se existe service provider, emitir erro com UseServiceProvider 
            // para ser chamado
            return SDKConfiguration.ServiceProvider.GetService(type);
        }
        
        public void Run(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent) 
        {
            // TODO validar parâmetros

            var context = _contextBuilder.Build(payload, eventName);

            if (context == null) {
                // TODO mensagem de erro
                throw new Exception("Erro ao tentar obter worker.");
            }

            Console.WriteLine("############# context.Event.Name: " + context.GetEvent().Name);
        
            MethodInfo methodInfo = SDKConfiguration.GetMethodByEvent(context.GetEvent().Name);
            /*if (SDKConfiguration.Binds.ContainsKey(eventName)) {
                methodInfo = SDKConfiguration.Binds[eventName];
            }*/

            if (methodInfo == null) {
                // TODO mensagem de erro
                throw new Exception("Erro ao tentar obter worker.");
            }
        
            var runner = GetRunner(methodInfo.DeclaringType);    

            if (runner == null) {
                // TODO mensagem de erro
                throw new Exception("methodInfo.DeclaringType");
            }

            var args = methodInfo.GetParameters();

            // TODO apenas context de argumento
            methodInfo.Invoke(runner, new [] {context});
            //var args = methodInfo.GetParameters();
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

        public void Run(string instanceId)
        {
            if (string.IsNullOrEmpty(instanceId)) {
                throw new SDKRuntimeException("Instance ID of process not found. ENV: INSTANCE_ID");
            }

            var context = _contextBuilder.Build(instanceId);

            if (context == null) {
                throw new SDKRuntimeException(
                    string.Format("Error building instance context. INSTANCE_ID={0}", instanceId));
            }

            var methodInfo = SDKConfiguration.GetMethodByEvent(context.GetEvent().Name);
            
            if (methodInfo == null) {
                
                throw new SDKRuntimeException(
                    string.Format("Method not found to event. Event={0}", context.GetEvent().Name));
            }
        
            var runner = GetRunner(methodInfo.DeclaringType);    

            if (runner == null) {
                throw new SDKRuntimeException(
                    string.Format("Type not register to dependence injection. Type={0}", methodInfo.DeclaringType));
            }

            methodInfo.Invoke(runner, _resolveArgs(methodInfo, context));

            context.UpdateMemory();
            
            // TODO salvando na memória de cálculo
            
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };
            Console.WriteLine("##############3 memory: " + Newtonsoft.Json.JsonConvert.SerializeObject(context.Memory, settings));

            _processMemoryService.Commit(context.Memory);
        }
    }
}