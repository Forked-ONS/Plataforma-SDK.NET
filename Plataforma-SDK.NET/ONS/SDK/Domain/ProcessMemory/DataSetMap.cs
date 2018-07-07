using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ONS.SDK.Domain.ProcessMemmory
{
    public class DataSetMap
    {
        [JsonProperty("entities")]
        public Dictionary<string, List<object>> Entities {get;set;}
    }
}