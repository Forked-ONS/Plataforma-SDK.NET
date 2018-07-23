using ONS.SDK.Domain.Base;

namespace ONS.SDK.Domain.Core
{
    /// <summary>
    /// Representa um process do tipo presentation do core da plataforma.
    /// </summary>
    public class Presentation : Model
    {
        public string Name {get;set;}
        public string SystemId {get;set;}
        public string Url {get;set;}
        public string Version {get;set;}
        public string Image {get;set;}

        public Presentation(){
            this._Metadata = new Metadata() {
                Type="presentation"
            };
        }
    }
}