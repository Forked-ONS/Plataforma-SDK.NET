using System.Collections.Generic;

namespace ONS.SDK.Services {

    public class Criteria {
        public string FilterName { get; set; }
        public List<Parameter> Parameters { get; set; }

        public Criteria () {
            this.Parameters = new List<Parameter> ();
        }
    }

    public class Parameter {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}