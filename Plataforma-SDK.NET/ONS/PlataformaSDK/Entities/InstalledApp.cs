using ONS.PlataformaSDK.Domain;

namespace ONS.PlataformaSDK.Entities
{
    public class InstalledApp : BaseEntity
    {
        public string Host {get; set;}
        public string Name {get; set;}
        public string Port {get; set;}
        public string SystemId{get;set;}
        public string Type{get;set;}
    }
}