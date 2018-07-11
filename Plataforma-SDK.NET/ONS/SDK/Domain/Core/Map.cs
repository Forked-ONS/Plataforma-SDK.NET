using System.Collections.Generic;
using Newtonsoft.Json;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Domain.Core {
    public class Map : Model {
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("processId")]
        public string ProcessId { get; set; }
        
        [JsonProperty("systemId")]
        public string SystemId { get; set; }
        
        [JsonProperty("content")]
        public string Content { get; set; }

        public override bool Equals (object obj) {
            var map = obj as Map;
            return map != null &&
                Id == map.Id &&
                Name == map.Name &&
                ProcessId == map.ProcessId &&
                SystemId == map.SystemId &&
                Content == map.Content;
        }

        public override int GetHashCode () {
            var hashCode = 1500394392;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode (Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode (Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode (ProcessId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode (SystemId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode (Content);
            return hashCode;
        }

        public override string ToString() {
            return $"[{this.GetType().Name}] {{ Id={this.Id}, Name={this.Name}, ProcessId={this.ProcessId}, SystemId={this.SystemId} }}";
        }
    }
}