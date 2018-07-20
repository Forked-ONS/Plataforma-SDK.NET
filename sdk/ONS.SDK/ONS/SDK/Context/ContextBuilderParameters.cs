using ONS.SDK.Worker;

namespace ONS.SDK.Context
{
    /// <summary>
    /// Representa os parâmetros passaddos para o construtor de contexto, 
    /// para que possa criar e configurar corretamente esse contexto.
    /// </summary>
    public class ContextBuilderParameters {

        /// <summary>
        /// Define os parâmetros do evento para execução de negócio, para montar o contexto.
        /// </summary>
        public IPayload Payload {get;private set;}

        /// <summary>
        /// Nome do evento para contrução do contexto.
        /// </summary>
        public string EventName {get;private set;}

        public ContextBuilderParameters(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent) {
            this.Payload = payload;
            this.EventName = eventName;
        }

        public override string ToString() {
            return $"{this.GetType().Name}[Payload={Payload}, EventName={EventName}]";
        }
    }
}