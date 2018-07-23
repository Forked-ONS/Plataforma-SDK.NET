using ONS.SDK.Domain.Base;

namespace ONS.SDK.Domain.Core {

    /// <summary>
    /// Representa uma operação de processo do core da plataforma.
    /// </summary>
    public class Operation : Model {
        
        public bool Commit { get; set; }
        public string Event_In { get; set; }
        public string Event_Out { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string ProcessId { get; set; }
        public bool Reprocessable { get; set; }
        public string SystemId { get; set; }
        public string Version { get; set; }

        public Operation () {
            this._Metadata = new Metadata () {
                Type = "operation"
            };
        }

    }
}