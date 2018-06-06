namespace ONS.SDK.Domain.Core {
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