namespace ONS.SDK.Domain.Core {
    public class InstalledApp : Model {
        public string Host { get; set; }
        public string Name { get; set; }
        public int Port { get; set; }
        public string SystemId { get; set; }
        public string Type { get; set; }
        public InstalledApp () {
            this._Metadata = new Metadata () {
                Type = "installedApp"
            };
        }
    }
}