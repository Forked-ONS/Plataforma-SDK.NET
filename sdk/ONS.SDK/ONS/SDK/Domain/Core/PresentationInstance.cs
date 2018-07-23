using ONS.SDK.Domain.Base;

namespace ONS.SDK.Domain.Core {

    /// <summary>
    /// Representa uma instância do processo do tipo presentation do core da plataforma.
    /// </summary>
    public class PresentationInstance : Model {
        
        public string PresentationId { get; set; }
        public PresentationInstance () {
            this._Metadata = new Metadata () {
                Type = "presentationInstance"
            };
        }
    }
}