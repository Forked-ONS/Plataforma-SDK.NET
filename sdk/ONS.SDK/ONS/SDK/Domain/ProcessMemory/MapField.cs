using System.Collections.Generic;
using Newtonsoft.Json;

namespace ONS.SDK.Domain.ProcessMemmory {

    /// <summary>
    /// Representa o mapa de colunas de um mapa de entidade na memória de processamento.
    /// </summary>
    public class MapField {
        
        [JsonProperty("column")]
        public string Column { get; set; }
    }
}