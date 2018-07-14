using System.Collections.Generic;
using Newtonsoft.Json;

namespace ONS.SDK.Domain.ProcessMemmory {
    
    public class MapItem {
        
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("fields")]
        public Dictionary<string, MapField> Fields { get; set; }
        
        [JsonProperty("filters")]
        public Dictionary<string, string> Filters { get; set; }
    }
}