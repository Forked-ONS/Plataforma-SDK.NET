using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ONS.SDK.Domain.ProcessMemmory {
    
    public class MemoryEvent 
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }

        [JsonProperty("reference_date")]
        public DateTime? ReferenceDate { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("branch")]
        public string Branch { get; set; }

        [JsonProperty("reproduction")]
        public Reproduction Reproduction { get; set; }

        [JsonProperty("reprocessing")]
        public Reprocess Reprocess { get; set; }

        [JsonProperty("payload")]
        public object Payload { get; set; }

    }
}