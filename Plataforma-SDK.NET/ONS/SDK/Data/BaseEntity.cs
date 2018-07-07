using Newtonsoft.Json;

namespace ONS.SDK.Data
{
    public abstract class BaseEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

    }
}