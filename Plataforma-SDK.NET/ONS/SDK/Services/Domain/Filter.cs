using System.Collections.Generic;

namespace ONS.SDK.Services.Domain {
    
    public class Filter {

        public Filter() {
            Parameters = new Dictionary<string, object>();
        }

        public string Map { get; set; }
        
        public string Entity { get; set; }
        
        public string Name { get; set; }
        
        public IDictionary<string, object> Parameters { get; set; }
        
    }
}