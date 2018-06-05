using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace ONS.SDK.Domain.Core {
    public class Event<Params> {
        public string Name { get; set; }
        public string Scope { get; set; }
        public string instanceId { get; set; }
        public string Reference_Date { get; set; }
        public string Tag { get; set; }
        public Reproduction Reproduction { get; set; }
        public Reprocess Reprocess { get; set; }
        public Params Payload { get; set; }

    }
}