using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ONS.SDK.Domain.Core {
    public class Memory {

        [JsonProperty("event")]
        public MemoryEvent Event { get; set; }

        [JsonProperty("processId")]
        public string ProcessId { get; set; }
        
        [JsonProperty("systemId")]
        public string SystemId { get; set; }
        
        [JsonProperty("instanceId")]
        public string InstanceId { get; set; }
        
        [JsonProperty("eventOut")]
        public string EventOut { get; set; }
        
        [JsonProperty("commit")]
        public bool Commit { get; set; }
        
        [JsonProperty("map")]
        public ProcessMap Map { get; set; }
        
        [JsonProperty("dataset")]
        public DataSetMap DataSet { get; set; }
    }
}