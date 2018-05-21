using System.Collections.Generic;

namespace ONS.PlataformaSDK.Entities
{
    public class Filter
    {
        public string Name { get; set; }
        public string Query { get; set; }
        public bool ShouldBeExecuted { get; set; }
        public Dictionary<string, object> Parameters { get; set; }

        public Filter(string name, string query)
        {
            this.Name = name;
            this.Query = query;
            Parameters = new Dictionary<string, object>();
        }

        public override bool Equals(object obj)
        {
            var filter = obj as Filter;
            return filter != null &&
                   Name == filter.Name &&
                   Query == filter.Query &&
                   ShouldBeExecuted == filter.ShouldBeExecuted &&
                   EqualityComparer<Dictionary<string, object>>.Default.Equals(Parameters, filter.Parameters);
        }

        public override int GetHashCode()
        {
            var hashCode = -573331380;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Query);
            hashCode = hashCode * -1521134295 + ShouldBeExecuted.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<string, object>>.Default.GetHashCode(Parameters);
            return hashCode;
        }
    }
}