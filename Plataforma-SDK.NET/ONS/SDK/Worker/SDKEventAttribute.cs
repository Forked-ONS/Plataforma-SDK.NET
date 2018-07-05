using System;

namespace ONS.SDK.Worker
{
    public class SDKEventAttribute: Attribute
    {
        public const string DefaultEvent = "DefaultEvent";

        public string EventName {get;private set;}
        
        public SDKEventAttribute(string eventName = DefaultEvent) {
            EventName = eventName;
        }
    }
}