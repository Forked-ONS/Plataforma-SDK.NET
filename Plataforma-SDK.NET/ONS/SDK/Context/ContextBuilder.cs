/**
Context builder é a classe responsável por montar um objeto de contexto de execução
*/
using System;
using Newtonsoft.Json.Linq;
using ONS.SDK.Configuration;
using ONS.SDK.Services;
using ONS.SDK.Worker;

namespace ONS.SDK.Context {

    public class ContextBuilder {

        private IProcessMemoryService _processMemory;

        public ContextBuilder () { }

        public ContextBuilder(IProcessMemoryService processMemory) => 
            this._processMemory = processMemory;

        public IContext Build(string instanceId) {

            // TODO colocar logs
            var memory = _processMemory.Head(instanceId);

            var eventName = memory.Event.Name;

            var typePayload = SDKConfiguration.GetTypePayload(eventName);

            var typeContext = typeof(SDKContext<>).MakeGenericType(typePayload);
            
            var context = (IContext) Activator.CreateInstance(typeContext, memory);

            JObject jobjPayload = context.GetEvent().MemoryEvent.Payload as JObject;
            if (jobjPayload != null) {
                context.GetEvent().SetPayload((IPayload) jobjPayload.ToObject(typePayload));
            }

            return context;
        }

        public IContext Build(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent) {

            // TODO ajustar para construir a api para salvar
            var memory = _processMemory.Head("User");

            // TODO carregar evento
            var typePayload = payload.GetType();

            var typeContext = typeof(SDKContext<>).MakeGenericType(typePayload);
            
            var context = (IContext) Activator.CreateInstance(typeContext, memory);

            return context;
        }
        
    }
}