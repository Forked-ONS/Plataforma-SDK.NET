using System.Collections.Generic;
using Newtonsoft.Json;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Domain.ProcessMemmory {
    
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