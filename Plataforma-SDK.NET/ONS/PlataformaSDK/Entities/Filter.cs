using System.Collections.Generic;

namespace ONS.PlataformaSDK.Entities
{
    public class Filter
    {
        public string Name { get; set; }
        public string Query { get; set; }
        public bool ShouldBeExecuted{ get; set; }
        public Dictionary<string, object> Parameters{ get; set; }

        public Filter(string name, string query)
        {
            this.Name = name;
            this.Query = query;
            Parameters = new Dictionary<string, object>();
        }
    }
}