using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ONS.PlataformaSDK.Entities
{
    public class Event
    {
        public string Name { get; set; }
        public string Scope { get; set; }
        public string Instance_Id { get; set; }
        public string Reference_Date { get; set; }
        public JObject Reproduction { get; set; }
        public JObject Reprocess { get; set; }
        public dynamic Payload { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Event other = (Event)obj;

            return (this.Name != null && other.Name != null && this.Name.Equals(other.Name)) &&
                   (this.Scope != null && other.Scope != null && this.Scope.Equals(other.Scope)) &&
                   (this.Instance_Id != null && other.Instance_Id != null && this.Instance_Id.Equals(other.Instance_Id)) &&
                   (this.Reference_Date != null && other.Reference_Date != null && this.Reference_Date.Equals(other.Reference_Date)) &&
                   (this.Reproduction != null && other.Reproduction != null && this.Reproduction.Equals(other.Reproduction)) &&
                   (this.Reprocess != null && other.Reprocess != null && this.Reprocess.Equals(other.Reprocess)) &&
                   (this.Payload != null && other.Payload != null && this.Payload.Equals(other.Payload));
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            var hashCode = 181846194;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Scope);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Instance_Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Reference_Date);
            hashCode = hashCode * -1521134295 + EqualityComparer<JObject>.Default.GetHashCode(Reproduction);
            hashCode = hashCode * -1521134295 + EqualityComparer<JObject>.Default.GetHashCode(Reprocess);
            hashCode = hashCode * -1521134295 + EqualityComparer<dynamic>.Default.GetHashCode(Payload);
            return hashCode;

        }

    }
}
