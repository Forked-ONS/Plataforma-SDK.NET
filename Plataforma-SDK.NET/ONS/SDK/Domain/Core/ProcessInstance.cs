using System;

namespace ONS.SDK.Domain.Core {
    public class ProcessInstance : Model {
        public string Baseline { get; set; }
        public bool IsFork { get; set; }
        public string Origin_Event_Name { get; set; }
        public string ProcessId { get; set; }
        public string Scope { get; set; }
        public DateTime StartExecution { get; set; }
        public string Status { get; set; }
        public string SystemId { get; set; }
        public string Version { get; set; }
        public ProcessInstance () {
            this._Metadata = new Metadata () {
                Type = "processInstance"
            };
        }
    }
}