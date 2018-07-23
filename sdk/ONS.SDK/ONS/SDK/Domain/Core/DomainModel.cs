using ONS.SDK.Domain.Base;

namespace ONS.SDK.Domain.Core {

    /// <summary>
    /// Representa a entidade de modelo de domínio do core da plataforma.
    /// </summary>
    public class DomainModel : Model {
        public string Model { get; set; }
        public string Name { get; set; }
        public string SystemId { get; set; }
        public string Version { get; set; }
        public DomainModel () {
            this._Metadata = new Metadata () {
                Type = "domain_model"
            };
        }
    }
}