using System.Collections.Generic;

namespace ONS.PlataformaSDK.Domain
{
    public class Metadata
    {
        public string Branch { get; set; }
        public string Type { get; set; }
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