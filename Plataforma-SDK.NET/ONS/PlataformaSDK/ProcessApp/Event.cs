using Newtonsoft.Json.Linq;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class Event
    {
        public string Name{get; set;}
        public string Scope{get; set;}
        public JObject Payload{get; set;}
    }
}
