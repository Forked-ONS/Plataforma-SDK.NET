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

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var Other = (Filter)obj;

            return this.Name.Equals(Other.Name) && this.Query.Equals(Other.Query) && this.ShouldBeExecuted == Other.ShouldBeExecuted &&
                this.Parameters.Equals(Other.Parameters);
        }

        public override int GetHashCode()
        {
            var hashCode = 181846194;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Query);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(ShouldBeExecuted);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<string, object>>.Default.GetHashCode(Parameters);
            return hashCode;
        }


    }
}