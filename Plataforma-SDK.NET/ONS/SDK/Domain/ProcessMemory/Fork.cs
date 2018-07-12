using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ONS.SDK.Domain.ProcessMemmory {
    
    public class Fork {

        public static string StatusOpen = "open";

        public static string OwnerAnonymous = "anonymous";

        public Fork() {
            this.Status = StatusOpen;
            this.Owner = OwnerAnonymous;
            this.StartedAt = DateTime.Now;
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("startedAt")]
        public DateTime? StartedAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }
    }
}