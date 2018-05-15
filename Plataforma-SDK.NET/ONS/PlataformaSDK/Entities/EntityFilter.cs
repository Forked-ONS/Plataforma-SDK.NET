using System.Collections.Generic;

namespace ONS.PlataformaSDK.Entities
{
    public class EntityFilter
    {
        public string MapName { get; set; }
        public string EntityName { get; set; }
        public List<Filter> Filters { get; set; }

        public EntityFilter()
        {
            Filters = new List<Filter>();
        }

        public void addFilter(Filter filter)
        {
            Filters.Add(filter);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var Other = (EntityFilter)obj;

            return this.MapName.Equals(Other.MapName) && this.EntityName.Equals(Other.EntityName) && this.Filters.Equals(Other.Filters);
        }

        public override int GetHashCode()
        {
            var hashCode = 181846194;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MapName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EntityName);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Filter>>.Default.GetHashCode(Filters);
            return hashCode;
        }
    }
}