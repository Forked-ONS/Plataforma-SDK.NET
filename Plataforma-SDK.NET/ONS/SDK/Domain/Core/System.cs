namespace ONS.SDK.Domain.Core {
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