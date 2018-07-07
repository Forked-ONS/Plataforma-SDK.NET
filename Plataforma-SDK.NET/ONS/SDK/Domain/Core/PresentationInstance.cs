using ONS.SDK.Domain.Base;

namespace ONS.SDK.Domain.Core {
    public class PresentationInstance : Model {
        public string PresentationId { get; set; }
        public PresentationInstance () {
            this._Metadata = new Metadata () {
                Type = "presentationInstance"
            };
        }
    }
}