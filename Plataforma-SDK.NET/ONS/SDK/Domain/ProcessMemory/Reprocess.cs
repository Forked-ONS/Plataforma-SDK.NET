using Newtonsoft.Json;

namespace ONS.SDK.Domain.ProcessMemmory {
    
    public class Reprocess {
        
        [JsonProperty("from")]
        public string From;

        [JsonProperty("to")]
        public string To;
    }
}