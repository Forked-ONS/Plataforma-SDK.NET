using ONS.SDK.Data;
using ONS.SDK.Context;
using ONS.SDK.Domain.Base;
using Newtonsoft.Json;

namespace ONS.SDK.Test.Web.Entities
{
    [DataMap("Conta")]
    public class Conta: Model, IPayload
    {
        public Conta(): base(true) {

        }

        [JsonProperty("name")]
        public string Name {get;set;}

        [JsonProperty("balance")]
        public int? Balance {get;set;}

        public override string ToString() {
            return $"{this.GetType().Name}[Id={Id}, Name={Name}, Balance={Balance}]";
        }
    }
}