using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ONS.SDK.Utils.Http {
    public class JsonHttpClient {
        private readonly HttpClient _client = new HttpClient ();

        public JsonHttpClient () { }

        public JsonHttpClient (HttpClient client) {
            this._client = client;
        }
        private string serialize (object obj) {
            string json = JsonConvert.SerializeObject (obj, new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver ()
            });
            return json;
        }

        private T deserialize<T>(string json) {
            return JsonConvert.DeserializeObject<T> (json, new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver ()
            });
        }

        public T Post<T> (string url, object body) {
            var resp = _client.Post (url, serialize(body));
            var obj = resp.Result;
            return  deserialize<T>(obj);
        }

        public T Put<T> (string url, object body) {
            var resp = _client.Put (url, serialize(body));
            var obj = resp.Result;
            return deserialize<T>(obj);
        }

        public T Get<T> (string url) {
            var resp = _client.Get (url);
            var json = resp.Result;
            return  deserialize<T>(json);
        }
    }
}