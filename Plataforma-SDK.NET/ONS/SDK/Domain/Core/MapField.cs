using System.Collections.Generic;
using Newtonsoft.Json;

namespace ONS.SDK.Domain.Core {
    public class MapField {
        
        [JsonProperty("column")]
        public string Column { get; set; }
    }
}