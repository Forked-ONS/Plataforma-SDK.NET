using System.Collections.Generic;
using Newtonsoft.Json;

namespace ONS.PlataformaSDK.Entities
{
    public class Reproduction
    {
        [JsonProperty("from")]
        public string From{get;set;}
        [JsonProperty("to")]
        public string To{get;set;}

        public override bool Equals(object obj)
        {
            var @reproduction = obj as Reproduction;
            return @reproduction != null &&
                   From == @reproduction.From &&
                   To == @reproduction.To;
        }

        public override int GetHashCode()
        {
            var hashCode = -629850613;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(From);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(To);
            return hashCode;
        }
    }
}