using System;
using Newtonsoft.Json;

namespace ONS.SDK.Domain.Base {
    
    public class Metadata {
        
        [JsonProperty("branch")]
        public string Branch { get; set; }
        
        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("origin")]
        public string Origin { get; set; }
        
        [JsonProperty("modified_at")]
        public DateTime? ModifiedAt { get; set; }
        
        [JsonProperty("changeTrack")]
        public string ChangeTrack { get; set; }
    }
}