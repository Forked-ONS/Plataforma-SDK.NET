using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ONS.SDK.Utils.Http {
    public class HttpClient {
        private static readonly System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient ();

        async public virtual Task<string> Get (string url, params Header[] headers) {
            return await Do(HttpMethod.Get, url, "", headers);
        }

        async public virtual Task<string> Post (string url, string json, params Header[] headers) {
            return await Do(HttpMethod.Post, url, json, headers);
        }

        async public virtual Task<string> Put (string url, string json, params Header[] headers) {
            return await Do(HttpMethod.Put, url, json, headers);
        }

        async public virtual Task<string> Do (HttpMethod method, string url, string json, params Header[] headers) {
            var content = new StringContent (json, Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage (method, url);
            foreach (var header in headers) {
                requestMessage.Headers.Add (header.Key, new List<string> () { header.Value });
            }
            requestMessage.Content = content;
            using (requestMessage) {
                var response = await _client.SendAsync (requestMessage);
                response.EnsureSuccessStatusCode ();
                string responseBody = await response.Content.ReadAsStringAsync ();
                return responseBody;
            }
        }

    }
}