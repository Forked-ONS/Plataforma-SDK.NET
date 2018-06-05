using System.Collections.Generic;

namespace ONS.SDK.Domain.Core {
    public class MapItem {
        public string Model { get; set; }
        public Dictionary<string, MapField> Fields { get; set; }
        public Dictionary<string, string> Filters { get; set; }
    }
}