using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ONS.SDK.Utils.Http {
    public class HttpClient {
        private static readonly System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient ();

        async public virtual Task<string> Get (string url) {
            var response = await _client.GetAsync (url);
            response.EnsureSuccessStatusCode ();
            string responseBody = await response.Content.ReadAsStringAsync ();
            return responseBody;
        }

        async public virtual Task<string> Post (string url, string json) {
            var content = new StringContent (json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync (url, content);
            response.EnsureSuccessStatusCode ();
            return await response.Content.ReadAsStringAsync ();
        }

        async public virtual Task<string> Put (string url, string json) {
            var content = new StringContent (json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync (url, content);
            response.EnsureSuccessStatusCode ();
            return await response.Content.ReadAsStringAsync ();
        }

    }
}