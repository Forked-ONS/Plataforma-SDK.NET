using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ONS.SDK.Utils.Http {
    public class JsonHttpClient {

        private readonly ILogger _logger;

        private readonly HttpClient _client;

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings {
            NullValueHandling = NullValueHandling.Ignore
        };

        public JsonHttpClient(ILogger<HttpClient> logger, HttpClient httpClient) {
            _logger = logger;
            _client = httpClient;
        }

        public JsonHttpClient () { }

        public JsonHttpClient (HttpClient client) {
            this._client = client;
        }
        private string serialize (object obj) {
            string json = JsonConvert.SerializeObject (obj, _jsonSettings);
            return json;
        }

        private T deserialize<T>(string json) {
            if (json == null) {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T> (json, _jsonSettings);
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
            return deserialize<T>(json);
        }
    }
}