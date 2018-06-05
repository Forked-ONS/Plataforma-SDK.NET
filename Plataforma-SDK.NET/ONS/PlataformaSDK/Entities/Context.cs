using System.Collections.Generic;
using Newtonsoft.Json;
using ONS.PlataformaSDK.Domain;

namespace ONS.PlataformaSDK.Entities
{
    public class Context
    {
        [JsonProperty("event")]
        public Event Event{get; set;}
        [JsonProperty("processId")]
        public string ProcessId{get; set;}
        [JsonProperty("systemId")]
        public string SystemId{get; set;}
        [JsonProperty("instanceId")]
        public string InstanceId{get; set;}
        [JsonProperty("eventOut")]
        public string EventOut{get; set;}
        [JsonProperty("commit")]
        public bool Commit{get; set;}
        [JsonProperty("map")]
        public PlatformMap Map{get; set;}

        [JsonProperty("dataset")]
        public DataSet DataSet{get; set;}

        public override bool Equals(object obj)
        {
            var context = obj as Context;
            return context != null &&
                   EqualityComparer<Event>.Default.Equals(Event, context.Event) &&
                   ProcessId == context.ProcessId &&
                   SystemId == context.SystemId &&
                   InstanceId == context.InstanceId &&
                   EventOut == context.EventOut &&
                   Commit == context.Commit &&
                   EqualityComparer<PlatformMap>.Default.Equals(Map, context.Map);
        }


        public override int GetHashCode()
        {
            var hashCode = 161774916;
            hashCode = hashCode * -1521134295 + EqualityComparer<Event>.Default.GetHashCode(Event);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProcessId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SystemId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(InstanceId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EventOut);
            hashCode = hashCode * -1521134295 + Commit.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<PlatformMap>.Default.GetHashCode(Map);
            return hashCode;
        }
    }
}