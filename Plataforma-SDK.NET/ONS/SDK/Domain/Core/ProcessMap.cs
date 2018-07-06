using System.Collections.Generic;
using Newtonsoft.Json;

namespace ONS.SDK.Domain.Core {
    public class ProcessMap : Model {
        
        [JsonProperty("content")]
        public Dictionary<string, MapItem> Content { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("processId")]
        public string ProcessId { get; set; }
        
        [JsonProperty("systemId")]
        public string SystemId { get; set; }
    }
}