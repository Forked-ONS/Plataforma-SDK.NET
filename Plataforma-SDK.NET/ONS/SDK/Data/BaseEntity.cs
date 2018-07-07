using Newtonsoft.Json;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data
{
    public abstract class BaseEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("_metadata")]
        internal Metadata _Metadata { get; set; }
    }
}