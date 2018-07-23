namespace ONS.SDK.Domain.Core {

    /// <summary>
    /// Representa as informações de uma aplicação no core da plataforma.
    /// </summary>
    public class ProcessInfo {
        
        public string ProcessInstanceId { get; set; }

        public string ProcessId { get; set; }

        public string SystemId { get; set; }

        public string EventIn { get; set; }

        public bool PersistDomainSync { get; set; }

    }
}