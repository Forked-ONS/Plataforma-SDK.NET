using System.Collections.Generic;

namespace ONS.SDK.Domain.Core {
    public class ProcessMap {
        public Dictionary<string, MapItem> Content { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
        public string ProcessId { get; set; }
        public string SystemId { get; set; }
    }
}