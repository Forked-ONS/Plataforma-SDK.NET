using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Microsoft.Extensions.Configuration;
using ONS.SDK.Configuration;
using ONS.SDK.Context;

namespace ONS.SDK.Worker
{
    public class SDKWorker
    {
        private readonly ContextBuilder _contextBuilder;

        public SDKWorker(ContextBuilder contextBuilder) {
            _contextBuilder = contextBuilder;
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

        public void Run(string instanceId)
        {
            if (string.IsNullOrEmpty(instanceId)) {
                throw new SDKRuntimeException("Instance ID of process not found. ENV: INSTANCE_ID");
            }

            var context = _contextBuilder.Build(instanceId);

            if (context == null) {
                // TODO mensagem de erro
                throw new SDKRuntimeException(
                    string.Format("Error building instance context. INSTANCE_ID={0}", instanceId));
            }

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
    }
}