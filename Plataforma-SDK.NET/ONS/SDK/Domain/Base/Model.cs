using System;
using Newtonsoft.Json;

namespace ONS.SDK.Domain.Base {
    public class Model {

        public Model() {}

        public Model(bool generateId) {
            this.Id = Guid.NewGuid().ToString();
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("_metadata")]
        internal Metadata _Metadata { get; set; }
    }
}