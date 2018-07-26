using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ONS.SDK.Context;
using ONS.SDK.Log;
using ONS.SDK.Worker;

namespace ONS.SDK.Utils.Http {

    /// <summary>
    /// Define um client http para o SDK.
    /// </summary>
    public class HttpClient 
    {
        private readonly ILogger _logger;
        private readonly IExecutionContext _executionContext;

        /// <summary>
        /// Construtor do HttpCliente.
        /// </summary>
        /// <param name="logger">Parâmetro do construtor</param>
        /// <param name="executionContext">Parâmetro do construtor</param>
        public HttpClient(ILogger<HttpClient> logger, IExecutionContext executionContext) {
            this._logger = logger;
            this._executionContext = executionContext;

        }

        /// <summary>
        /// Executa um requisição com método Get do protocolo http.
        /// </summary>
        /// <param name="url">Url da requisição</param>
        /// <param name="headers">Cabeçalho para ser adicionado na requisição</param>
        /// <returns>Conteúdo da resposta à requsisição</returns>
        async public virtual Task<string> Get (string url, params Header[] headers) {
            return await Do(HttpMethod.Get, url, "", headers);
        }

        /// <summary>
        /// Executa um requisição com método Post do protocolo http.
        /// </summary>
        /// <param name="url">Url da requisição</param>
        /// <param name="json">Conteúdo do corpo da requisição</param>
        /// <param name="headers">Cabeçalho para ser adicionado na requisição</param>
        /// <returns>Conteúdo da resposta à requsisição</returns>
        async public virtual Task<string> Post (string url, string json, params Header[] headers) {
            return await Do(HttpMethod.Post, url, json, headers);
        }

        /// <summary>
        /// Executa um requisição com método Put do protocolo http.
        /// </summary>
        /// <param name="url">Url da requisição</param>
        /// <param name="json">Conteúdo do corpo da requisição</param>
        /// <param name="headers">Cabeçalho para ser adicionado na requisição</param>
        /// <returns>Conteúdo da resposta à requsisição</returns>
        async public virtual Task<string> Put (string url, string json, params Header[] headers) {
            return await Do(HttpMethod.Put, url, json, headers);
        }

        /// <summary>
        /// Executa um requisição http com método informado.
        /// </summary>
        /// <param name="method">Método da requisição http</param>
        /// <param name="url">Url da requisição</param>
        /// <param name="json">Conteúdo do corpo da requisição</param>
        /// <param name="headers">Cabeçalho para ser adicionado na requisição</param>
        /// <returns>Conteúdo da resposta à requsisição</returns>
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

                    if (this._executionContext != null && this._executionContext.ExecutionParameter != null) 
                    {
                        var execParam = this._executionContext.ExecutionParameter;

                        if (execParam.ReferenceDate.HasValue) {
                            requestMessage.Headers.Add(
                                "Reference-Date", 
                                new List<string>(){ Convert.ToString(execParam.ReferenceDate.Value) }
                            );
                        }
                        else if (!string.IsNullOrEmpty(execParam.Branch)) {
                            requestMessage.Headers.Add(
                                "Branch", 
                                new List<string>(){ Convert.ToString(execParam.Branch) }
                            );
                        }
                        else if (!string.IsNullOrEmpty(execParam.InstanceId)) {
                            requestMessage.Headers.Add(
                                "Instance-Id", 
                                new List<string>(){ Convert.ToString(execParam.InstanceId) }
                            );
                        }
                    }

                    requestMessage.Content = content;
                    using (requestMessage) {

                        var response = await _client.SendAsync (requestMessage);
                        
                        var statusCode = response.StatusCode;
                        var reason = response.ReasonPhrase;
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        
                        _ensureSuccessStatusCode(statusCode, responseBody, reason, url, response);    

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

        private System.Net.Http.HttpClient _createHttpClient()
        {
            return new System.Net.Http.HttpClient(
                new LogHandler(new HttpClientHandler(), _logger)
            );
        }

        
        private void _ensureSuccessStatusCode(
            HttpStatusCode statusCode, string responseBody, 
            string reason, string url, HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new SDKHttpException(
                    statusCode, 
                    responseBody, 
                    reason, 
                    $"Response status code does not indicate success: {statusCode} ({reason}). Url: {url}");
            }
        }

    }
}