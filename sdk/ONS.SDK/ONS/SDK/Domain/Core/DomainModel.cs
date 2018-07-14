using ONS.SDK.Domain.Base;

namespace ONS.SDK.Domain.Core {
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