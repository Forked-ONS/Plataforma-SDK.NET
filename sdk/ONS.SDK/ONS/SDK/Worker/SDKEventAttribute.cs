using System;

namespace ONS.SDK.Worker
{
    /// <summary>
    /// Utilizado para identificar os métodos de negócio para responder aos eventos do sistema.
    /// </summary>
    public class SDKEventAttribute: Attribute
    {
        /// <summary>
        /// Define o nome default de evento para representar qualquer evento do sistema.
        /// </summary>
        public const string DefaultEvent = "DefaultEvent";

        /// <summary>
        /// Nome do evento o qual o método deve atender.
        /// </summary>
        /// <value>Nome do evento ao qual o método deve responder.</value>
        public string EventName {get;private set;}
        
        public SDKEventAttribute(string eventName = DefaultEvent) {
            EventName = eventName;
        }
    }
}