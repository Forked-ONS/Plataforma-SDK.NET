using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ONS.SDK.Domain.Base;
using ONS.SDK.Domain.Core;
using ONS.SDK.Worker;
using YamlDotNet.Serialization;

namespace ONS.SDK.Domain.ProcessMemmory {
    
    public class ProcessMap : Model {
        
        [JsonProperty("content")]
        public Dictionary<string, MapItem> Content { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("processId")]
        public string ProcessId { get; set; }
        
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        public static ProcessMap ConvertFromMap(Map map) 
        {
            try {
                var processMap = new ProcessMap();

                processMap.Id = map.Id;
                processMap.Name = map.Name;
                processMap.ProcessId = map.ProcessId;
                processMap.SystemId = map.SystemId;
                processMap._Metadata = map._Metadata;

                processMap.Content = new Dictionary<string, MapItem>();
            
                var stringReader = new StringReader(map.Content);
                var deserializer = new DeserializerBuilder().Build();
                var yamlObject = deserializer.Deserialize<Dictionary<string, Dictionary<object, object>>>(stringReader);

                foreach (var yamlMap in yamlObject)
                {
                    var mapItem = new MapItem();
                    mapItem.Fields = new Dictionary<string, MapField>();

                    foreach (var yamlMapItem in yamlMap.Value) {

                        if (string.Equals(yamlMapItem.Key, "model")) {
                            mapItem.Model = yamlMapItem.Value as string;
                        }
                        else if (string.Equals(yamlMapItem.Key, "fields")) {
                            
                            var yamlFields = (Dictionary<object, object>) yamlMapItem.Value;
                            
                            foreach(var yamlFieldsItem in yamlFields) {
                                
                                var fieldKey = (string) yamlFieldsItem.Key;
                                var fieldValue = (Dictionary<object, object>) yamlFieldsItem.Value;

                                mapItem.Fields[fieldKey] = new MapField(){
                                    Column = (string) fieldValue["column"]
                                };
                            }
                        }
                        else if (string.Equals(yamlMapItem.Key, "filters")) {

                            var mapFilters = yamlMapItem.Value as Dictionary<object, object>;
                            var filters = new Dictionary<string,string>();
                            
                            if (mapFilters != null && mapFilters.Any()) {
                                foreach (var mapFilter in mapFilters) {
                                    filters[(string) mapFilter.Key] = (string) mapFilter.Value;
                                }
                                mapItem.Filters = filters;
                            }
                            
                        }

                    }

                    processMap.Content[yamlMap.Key] = mapItem;
                }

                return processMap;

            } catch(Exception ex) {
                throw new SDKRuntimeException($"Error trying to convert application maps to map of processing memory. map: {map}", ex);
            }
        }
    }
}