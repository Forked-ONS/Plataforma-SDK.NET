namespace ONS.PlataformaSDK.Entities
{
    public class Context
    {
        public Event Event{get; set;}
        public string ProcessId{get; set;}
        public string SystemId{get; set;}
        public string InstanceId{get; set;}
        public string EventOut{get; set;}
        public bool Commit{get; set;}
        public PlatformMap Map{get; set;}
    }
}
