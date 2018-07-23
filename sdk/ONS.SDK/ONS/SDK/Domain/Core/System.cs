using ONS.SDK.Domain.Base;

namespace ONS.SDK.Domain.Core {

    /// <summary>
    /// Representa o sistema, que contém tipos de processos no core da plataforma.
    /// </summary>
    public class System : Model {

        public string Description { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public System () {
            this._Metadata = new Metadata () {
                Type = "system"
            };
        }
    }
}