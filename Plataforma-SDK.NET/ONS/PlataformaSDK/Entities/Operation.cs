using System.Collections.Generic;

namespace ONS.PlataformaSDK.Entities
{
    public class Operation
    {
        public bool Commit{ get; set; }
        public string Event_In{ get; set; }
        public string Event_Out{ get; set; }
        public string Id{ get; set; }
        public string Image{ get; set; }   
        public string Name{ get; set; }
        public string ProcessId{ get; set; }
        public string SystemId{ get; set; }

        public override bool Equals(object obj)
        {
            var operation = obj as Operation;
            return operation != null &&
                   Commit == operation.Commit &&
                   Event_In == operation.Event_In &&
                   Event_Out == operation.Event_Out &&
                   Id == operation.Id &&
                   Image == operation.Image &&
                   Name == operation.Name &&
                   ProcessId == operation.ProcessId &&
                   SystemId == operation.SystemId;
        }

        public override int GetHashCode()
        {
            var hashCode = -774542657;
            hashCode = hashCode * -1521134295 + Commit.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Event_In);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Event_Out);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Image);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProcessId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SystemId);
            return hashCode;
        }
    }


}