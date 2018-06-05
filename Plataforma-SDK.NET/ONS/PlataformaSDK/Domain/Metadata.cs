using System.Collections.Generic;
using Newtonsoft.Json;

namespace ONS.PlataformaSDK.Domain
{
    public class Metadata
    {
        
        [JsonProperty("branch")]
        public string Branch { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("changeTrack")]
        public string ChangeTrack { get; set; }

        public Metadata(string branch, string type, string changeTrack)
        {
            this.Branch = branch;
            this.Type = type;
            this.ChangeTrack = changeTrack;
        }

        public override bool Equals(object obj)
        {
            var @metadata = obj as Metadata;
            return @metadata != null &&
                   Branch == @metadata.Branch &&
                   Type == @metadata.Type &&
                   ChangeTrack == @metadata.ChangeTrack;
        }

        public override int GetHashCode()
        {
            var hashCode = -629850613;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Branch);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ChangeTrack);
            return hashCode;
        }
    }
}