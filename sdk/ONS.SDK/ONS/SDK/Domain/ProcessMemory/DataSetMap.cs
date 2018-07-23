using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ONS.SDK.Domain.ProcessMemmory
{
    /// <summary>
    /// Representa o dataset da memória de processamento.
    /// </summary>
    public class DataSetMap
    {
        [JsonProperty("entities")]
        public Dictionary<string, object> Entities {get;set;}
    }
}