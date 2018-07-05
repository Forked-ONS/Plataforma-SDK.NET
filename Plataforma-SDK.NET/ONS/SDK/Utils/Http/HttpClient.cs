using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ONS.SDK.Log;

namespace ONS.SDK.Utils.Http {
    public class HttpClient 
    {
        private readonly ILogger _logger;

        public HttpClient(ILogger<HttpClient> logger) {
            _logger = logger;
        }

        async public virtual Task<string> Get (string url, params Header[] headers) {
            return await Do(HttpMethod.Get, url, "", headers);
        }

        async public virtual Task<string> Post (string url, string json, params Header[] headers) {
            return await Do(HttpMethod.Post, url, json, headers);
        }

        async public virtual Task<string> Put (string url, string json, params Header[] headers) {
            return await Do(HttpMethod.Put, url, json, headers);
        }

        private System.Net.Http.HttpClient _createHttpClient()
        {
            return new System.Net.Http.HttpClient(
                new LogHandler(new HttpClientHandler(), _logger)
            );
        }

        async public virtual Task<string> Do (HttpMethod method, string url, string json, params Header[] headers) {
            var content = new StringContent (json, Encoding.UTF8, "application/json");

            try {
                var watch = new Stopwatch();
                watch.Start();

                using (var _client = _createHttpClient()) 
                {
                    var requestMessage = new HttpRequestMessage (method, url);
                    foreach (var header in headers) {
                        requestMessage.Headers.Add (header.Key, new List<string> () { header.Value });
                    }
                    requestMessage.Content = content;
                    using (requestMessage) {
                        var response = await _client.SendAsync (requestMessage);
                        response.EnsureSuccessStatusCode ();
                        string responseBody = await response.Content.ReadAsStringAsync ();

                        if (_logger.IsEnabled(LogLevel.Debug))
                        {
                            var msg = string.Format(
                                "Requisição realizada com sucesso. Url={0}. Tempo[{1}ms]",
                                url, watch.ElapsedMilliseconds
                            );

                            _logger.LogDebug(msg);
                        }

                        return responseBody;
                    }
                }
                
            } catch(Exception ex) {

                var msg = string.Format("Erro ao executar a url informada. Url={0}", url);
                _logger.LogError(msg, ex);

                throw;
            }
        }

    }
}