using Newtonsoft.Json;
using ONS.PlataformaSDK.Domain;

namespace ONS.PlataformaSDK.Entities
{
        public class ContextMap : BaseEntity
    {
        [JsonProperty("name")]
        public string Name{get; set;}
        [JsonProperty("processId")]
        public string ProcessId{get; set;}
        [JsonProperty("systemId")]
        public string SystemId{get; set;}
        [JsonProperty("content")]
        public object Content{get; set;}

        public ContextMap(PlatformMap platformMap)
        {
            this.Id = platformMap.Id;
            this.Name = platformMap.Name;
            this.ProcessId = platformMap.ProcessId;
            this.SystemId = platformMap.SystemId;
            this._Metadata = platformMap._Metadata;
        }

    } 
}