using ONS.SDK.Data;
using ONS.SDK.Domain.Base;
using Newtonsoft.Json;

namespace ONS.SDK.Test.Web.Entities
{
    [DataMap("Conta")]
    public class Conta: Model
    {
        [JsonProperty("name")]
        public string Name {get;set;}

        [JsonProperty("balance")]
        public int? Balance {get;set;}
    }
}