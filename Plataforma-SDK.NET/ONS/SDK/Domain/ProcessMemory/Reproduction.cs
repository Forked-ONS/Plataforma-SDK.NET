using Newtonsoft.Json;

namespace ONS.SDK.Domain.ProcessMemmory {
    
    public class Reproduction {

        [JsonProperty("from")]
        public string From;
        
        [JsonProperty("to")]
        public string To;
    }
}