namespace ONS.SDK.Domain.Core {
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