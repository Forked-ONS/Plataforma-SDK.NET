using ONS.SDK.Domain.Base;

namespace ONS.SDK.Domain.Core {

    /// <summary>
    /// Representa a entidade de dependência de domínio do core da plataforma.
    /// </summary>
    public class DependencyDomain : Model {
        public string Entity { get; set; }
        public string Filter { get; set; }
        public string Name { get; set; }
        public string ProcessId { get; set; }
        public string SystemId { get; set; }
        public string Version { get; set; }
        public DependencyDomain () {
            this._Metadata = new Metadata () {
                Type = "dependencyDomain"
            };
        }
    }
}