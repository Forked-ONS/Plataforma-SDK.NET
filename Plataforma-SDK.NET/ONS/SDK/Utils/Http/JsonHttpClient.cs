using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ONS.SDK.Utils.Http {
    public class JsonHttpClient {

        private readonly ILogger _logger;

        private readonly HttpClient _client;

        public JsonHttpClient(ILogger<HttpClient> logger, HttpClient httpClient) {
            _logger = logger;
            _client = httpClient;
        }

        public JsonHttpClient () { }

        public JsonHttpClient (HttpClient client) {
            this._client = client;
        }
        private string serialize (object obj) {
            string json = JsonConvert.SerializeObject (obj, new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver (),
                    NullValueHandling = NullValueHandling.Ignore

            });
            return json;
        }

        private T deserialize<T> (string json) {
            if (json == null) {
                return default (T);
            }
            return JsonConvert.DeserializeObject<T> (json, new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver (),
                    NullValueHandling = NullValueHandling.Ignore
            });
        }

        public T Post<T> (string url, object body, params Header[] headers) {
            var resp = _client.Post (url, serialize (body), headers);
            var obj = resp.Result;
            return deserialize<T> (obj);
        }

        public T Put<T> (string url, object body, params Header[] headers) {
            var resp = _client.Put (url, serialize (body), headers);
            var obj = resp.Result;
            return deserialize<T> (obj);
        }

        public T Get<T> (string url, params Header[] headers) {
            var resp = _client.Get (url, headers);
            var json = resp.Result;
            return deserialize<T> (json);
        }
    }
}