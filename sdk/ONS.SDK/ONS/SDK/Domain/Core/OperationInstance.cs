using ONS.SDK.Domain.Base;

namespace ONS.SDK.Domain.Core {

    /// <summary>
    /// Representa uma instância de operação de processo do core da plataforma.
    /// </summary>
    public class OperationInstance : Model {

        public string EventName { get; set; }
        public string Image { get; set; }
        public bool MustCommit { get; set; }        
        public string OperationId { get; set; }
        public string ProcessId { get; set; }
        public string ProcessInstanceId { get; set; }
        public string Status { get; set; }
        public string SystemId { get; set; }
        public OperationInstance () {
            this._Metadata = new Metadata () {
                Type = "operationInstance"
            };
        }
    }
}