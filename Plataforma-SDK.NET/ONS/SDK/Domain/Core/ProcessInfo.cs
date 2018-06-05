namespace ONS.SDK.Domain.Core {
    public class ProcessInfo {
        public string ProcessInstanceId { get; set; }

        public string ProcessId { get; set; }

        public string SystemId { get; set; }

        public string EventIn { get; set; }

        public bool PersistDomainSync { get; set; }

    }
}