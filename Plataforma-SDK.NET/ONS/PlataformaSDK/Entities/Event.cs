using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ONS.PlataformaSDK.Entities
{
    public class Event
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("instanceId")]
        public string instanceId { get; set; }
        [JsonProperty("reference_date")]
        public string Reference_Date { get; set; }
        [JsonProperty("tag")]
        public string Tag { get; set; }
        [JsonProperty("reproduction")]
        public Reproduction Reproduction { get; set; }
        [JsonProperty("branch")]
        public string Branch { get; set; }
        [JsonProperty("reprocessing")]
        public JObject Reprocessing { get; set; }
        public JObject Payload { get; set; }

        public override bool Equals(object obj)
        {
            var @event = obj as Event;
            return @event != null &&
                   Name == @event.Name &&
                   Scope == @event.Scope &&
                   instanceId == @event.instanceId &&
                   Reference_Date == @event.Reference_Date &&
                   Tag == @event.Tag &&
                   Branch == @event.Branch &&
                   this.Reproduction.Equals(@event.Reproduction) &&
                   EqualityComparer<JObject>.Default.Equals(Reprocessing, @event.Reprocessing);
        }

        public override int GetHashCode()
        {
            var hashCode = -629850613;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Scope);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(instanceId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Reference_Date);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Branch);
            hashCode = hashCode * -1521134295 + EqualityComparer<Reproduction>.Default.GetHashCode(Reproduction);
            hashCode = hashCode * -1521134295 + EqualityComparer<JObject>.Default.GetHashCode(Reprocessing);
            return hashCode;
        }

    }
}