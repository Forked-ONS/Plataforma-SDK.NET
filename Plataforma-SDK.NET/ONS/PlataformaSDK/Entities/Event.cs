using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ONS.PlataformaSDK.Entities
{
    public class Event
    {
        public string Name { get; set; }
        public string Scope { get; set; }
        public string Instance_Id { get; set; }
        public string Reference_Date { get; set; }
        public JObject Reproduction { get; set; }
        public JObject Reprocess { get; set; }
        public dynamic Payload { get; set; }

        public override bool Equals(object obj)
        {
            var @event = obj as Event;
            return @event != null &&
                   Name == @event.Name &&
                   Scope == @event.Scope &&
                   Instance_Id == @event.Instance_Id &&
                   Reference_Date == @event.Reference_Date &&
                   EqualityComparer<JObject>.Default.Equals(Reproduction, @event.Reproduction) &&
                   EqualityComparer<JObject>.Default.Equals(Reprocess, @event.Reprocess);
        }

        public override int GetHashCode()
        {
            var hashCode = -629850613;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Scope);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Instance_Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Reference_Date);
            hashCode = hashCode * -1521134295 + EqualityComparer<JObject>.Default.GetHashCode(Reproduction);
            hashCode = hashCode * -1521134295 + EqualityComparer<JObject>.Default.GetHashCode(Reprocess);
            return hashCode;
        }
    }
}