using ONS.SDK.Worker;

namespace ONS.SDK.Context
{
    public class ContextBuilderParameters {
        
        public IPayload Payload {get;private set;}
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