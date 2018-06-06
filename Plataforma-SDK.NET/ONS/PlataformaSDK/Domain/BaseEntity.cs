using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ONS.PlataformaSDK.Domain
{
    public class BaseEntity
    {
        [JsonProperty("id")]
        public string Id{get;set;}
        
        [JsonProperty("_metadata")]
        public Metadata _Metadata{get; set;}

    }
}