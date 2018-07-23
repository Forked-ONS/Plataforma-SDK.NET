using System.Collections.Generic;
using Newtonsoft.Json;

namespace ONS.SDK.Domain.ProcessMemmory {
    
    /// <summary>
    /// Representa um item de mapa de entidade da memória de processamento.
    /// </summary>
    public class MapItem {
        
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("fields")]
        public Dictionary<string, MapField> Fields { get; set; }
        
        [JsonProperty("filters")]
        public Dictionary<string, string> Filters { get; set; }
    }
}