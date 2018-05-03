using Newtonsoft.Json.Linq;

namespace ONS.PlataformaSDK.Entities
{
    public class Event
    {
        public string Name{get; set;}
        public string Scope{get; set;}
        public string Instance_Id{get; set;}
        public string Reference_Date{get; set;}
        public JObject Reproduction{get; set;}
        public JObject Reprocess{get; set;}
        public JObject Payload{get; set;}

        
    }
}
