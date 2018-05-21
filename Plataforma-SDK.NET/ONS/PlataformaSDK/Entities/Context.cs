using System.Collections.Generic;

namespace ONS.PlataformaSDK.Entities
{
    public class Context
    {
        public Event Event{get; set;}
        public string ProcessId{get; set;}
        public string SystemId{get; set;}
        public string InstanceId{get; set;}
        public string EventOut{get; set;}
        public bool Commit{get; set;}
        public PlatformMap Map{get; set;}

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