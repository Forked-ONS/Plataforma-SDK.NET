using System.Collections.Generic;

namespace ONS.SDK.Services {

    /// <summary>
    /// Define critério de pesquisa.
    /// </summary>
    public class Criteria {
        public string FilterName { get; set; }
        public List<Parameter> Parameters { get; set; }

        public Criteria () {
            this.Parameters = new List<Parameter> ();
        }
    }

    /// <summary>
    /// Define parâmetros de critério de pesquisa.
    /// </summary>
    public class Parameter {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}