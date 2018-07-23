using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONS.SDK.Worker;

namespace ONS.SDK.Domain.ProcessMemmory {
    
    /// <summary>
    /// Evento original que disparou a execução do processamento, 
    /// ao qual se refere a memória.
    /// </summary>
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

        [JsonProperty("appOrigin")]
        public string AppOrigin { get; set; }

        [JsonProperty("reproduction")]
        public Reproduction Reproduction { get; set; }

        [JsonProperty("reprocessing")]
        public Reprocess Reprocess { get; set; }

        [JsonProperty("payload")]
        public object Payload { get; set; }

        [JsonIgnore]
        public bool IsReproduction {
            get {
                return Reproduction != null && 
                    !string.IsNullOrEmpty(Reproduction.From) &&
                    !string.IsNullOrEmpty(Reproduction.To);
            }
        }

        [JsonIgnore]
        public bool IsReprocess {
            get {
                return Reprocess != null && 
                    !string.IsNullOrEmpty(Reprocess.From) &&
                    !string.IsNullOrEmpty(Reprocess.To);
            }
        }

        public void Validate() {
            if (string.IsNullOrEmpty(Name)) {
                throw new SDKRuntimeException("Name is required in event.");
            }
            if (Payload == null) {
                throw new SDKRuntimeException("Payload is required in event.");
            }
        }

    }
}