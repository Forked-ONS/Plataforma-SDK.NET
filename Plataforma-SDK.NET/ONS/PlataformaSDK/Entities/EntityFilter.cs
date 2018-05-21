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

        public override bool Equals(object obj)
        {
            var filter = obj as EntityFilter;
            return filter != null &&
                   MapName == filter.MapName &&
                   EntityName == filter.EntityName &&
                   EqualityComparer<List<Filter>>.Default.Equals(Filters, filter.Filters);
        }

        public override int GetHashCode()
        {
            var hashCode = -1673476289;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MapName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EntityName);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Filter>>.Default.GetHashCode(Filters);
            return hashCode;
        }
    }
}