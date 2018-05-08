using System;
using ONS.PlataformaSDK.Entities;
using System.IO;
using YamlDotNet.Serialization;
using System.Collections.Generic;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class DataSetBuilder
    {
        public List<EntityFilter> EntitiesFilters;

        public DataSetBuilder()
        {
            EntitiesFilters = new List<EntityFilter>();
        }
        public virtual void Build(PlatformMap platformMap)
        {
            buildFilters(platformMap);
        }

        private void buildFilters(PlatformMap platformMap)
        {
            var StringReader = new StringReader(platformMap.Content);
            var deserializer = new DeserializerBuilder().Build();
            var YamlObject = deserializer.Deserialize<Dictionary<string, Dictionary<object, object>>>(StringReader);

            foreach (var key in YamlObject.Keys)
            {
                var EntityFilter = new EntityFilter();
                EntityFilter.EntityName = key;
                EntitiesFilters.Add(EntityFilter);

                var YamlFilter = YamlObject[key]["filters"];
                if(YamlFilter != null) {
                    var Filters = ((Dictionary<object, object>) YamlFilter);
                    foreach(var filterKey in Filters.Keys)
                    {
                        EntityFilter.addFilter(new Filter((string) filterKey, (string) Filters[filterKey]));
                    }
                }
            }
        }

    }

}