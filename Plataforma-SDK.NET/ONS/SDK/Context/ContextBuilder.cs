/**
Context builder é a classe responsável por montar um objeto de contexto de execução
*/
using System;
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

            var eventName = memory.Event.Value<string>("name");

            var typePayload = SDKConfiguration.GetTypePayload(eventName);

            var typeContext = typeof(SDKContext<>).MakeGenericType(typePayload);
            
            var context = (IContext) Activator.CreateInstance(typeContext);

            var typeEvent = typeof(SDKEvent<>).MakeGenericType(typePayload);

            context.SetEvent((IEvent) memory.Event.ToObject(typeEvent));//(IEvent) Activator.CreateInstance(typeEvent);

            return context;
        }

        public IContext Build(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent) {

            // TODO ajustar para construir a api para salvar
            var memory = _processMemory.Head("User");

            // TODO carregar evento
            var typePayload = payload.GetType();

            var typeContext = typeof(SDKContext<>).MakeGenericType(typePayload);
            
            var context = (IContext) Activator.CreateInstance(typeContext);

            var typeEvent = typeof(SDKEvent<>).MakeGenericType(typePayload);

            context.SetEvent((IEvent) memory.Event.ToObject(typeEvent));//(IEvent) Activator.CreateInstance(typeEvent);

            // TODO completar context
            /*context.Event = new SDKEvent() {
                Name = eventName,
                Payload = payload
            };*/

            return context;
        }
        
    }
}