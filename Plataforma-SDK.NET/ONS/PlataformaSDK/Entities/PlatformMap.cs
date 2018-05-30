using System.Collections.Generic;
using ONS.PlataformaSDK.Domain;

namespace ONS.PlataformaSDK.Entities
{
    public class PlatformMap : BaseEntity
    {
        public string Name{get; set;}
        public string ProcessId{get; set;}
        public string SystemId{get; set;}
        public string Content{get; set;}

        public override bool Equals(object obj)
        {
            var map = obj as PlatformMap;
            return map != null &&
                   Id == map.Id &&
                   Name == map.Name &&
                   ProcessId == map.ProcessId &&
                   SystemId == map.SystemId &&
                   Content == map.Content;
        }

        public override int GetHashCode()
        {
            var hashCode = 1500394392;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProcessId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SystemId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Content);
            return hashCode;
        }
    }
}