using ONS.SDK.Data;
using Newtonsoft.Json;

namespace ONS.SDK.Test.Web.Entities
{
    [DataMap("Conta")]
    public class Conta: BaseEntity
    {
        [JsonProperty("name")]
        public string Name {get;set;}

        [JsonProperty("balance")]
        public int? Balance {get;set;}
    }
}