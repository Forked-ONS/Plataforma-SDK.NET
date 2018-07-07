using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ONS.SDK.Domain.ProcessMemmory
{
    public class DataSetMap
    {
        [JsonProperty("entities")]
        public Dictionary<string, object> Entities {get;set;}
    }
}