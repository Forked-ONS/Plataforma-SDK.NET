using System.Collections.Generic;

namespace ONS.PlataformaSDK.Entities
{
    public class EntityFilter
    {
        public string MapName{get; set;}
        public string EntityName{get; set;}
        public List<Filter> Filters{get; set;}

        public EntityFilter()
        {
            Filters = new List<Filter>();
        }

        public void addFilter(Filter filter) 
        {
            Filters.Add(filter);
        }
    }
}