using System.Collections.Generic;
using Newtonsoft.Json;

namespace ONS.SDK.Domain.Core
{
    public class DataSetMap
    {
        [JsonProperty("entities")]
        public Dictionary<string, List<object>> Entities {get;set;}
    }
}