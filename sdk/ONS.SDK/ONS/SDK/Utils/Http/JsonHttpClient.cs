using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ONS.SDK.Utils.Http {

    /// <summary>
    /// Define um client http para o SDK, com suporte a conteúdo JSON.
    /// </summary>
    public class JsonHttpClient {

        private readonly ILogger _logger;

        private readonly HttpClient _client;

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings {
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// Construtor do JsonHttpClient.
        /// </summary>
        /// <param name="logger">Parâmetro do construtor</param>
        /// <param name="httpClient">Parâmetro do construtor</param>
        public JsonHttpClient(ILogger<HttpClient> logger, HttpClient httpClient) {
            _logger = logger;
            _client = httpClient;
        }

        /// <summary>
        /// Construtor do JsonHttpClient.
        /// </summary>
        public JsonHttpClient () { }

        /// <summary>
        /// Construtor do JsonHttpClient.
        /// </summary>
        /// <param name="client">Parâmetro do construtor</param>
        public JsonHttpClient (HttpClient client) {
            this._client = client;
        }
        
        /// <summary>
        /// Executa um requisição com método Post do protocolo http, 
        /// retornado o conteúdo JSON desserializado para o objeto c#.
        /// </summary>
        /// <param name="url">Url da requisição</param>
        /// <param name="body">Objeto que serializado para JSON e enviado no corpo da requisição</param>
        /// <param name="headers">Cabeçalho para ser adicionado na requisição</param>
        /// <typeparam name="T">Tipo do objeto JSON do conteúdo da reposta http</typeparam>
        /// <returns>Objeto desserializado</returns>
        
        public T Post<T> (string url, object body, params Header[] headers) {
            var resp = _client.Post (url, serialize (body), headers);
            var obj = resp.Result;
            return deserialize<T> (obj);
        }

        /// <summary>
        /// Executa um requisição com método Put do protocolo http, 
        /// retornado o conteúdo JSON desserializado para o objeto c#.
        /// </summary>
        /// <param name="url">Url da requisição</param>
        /// <param name="body">Objeto que serializado para JSON e enviado no corpo da requisição</param>
        /// <param name="headers">Cabeçalho para ser adicionado na requisição</param>
        /// <typeparam name="T">Tipo do objeto JSON do conteúdo da reposta http</typeparam>
        /// <returns>Objeto desserializado</returns>
        public T Put<T> (string url, object body, params Header[] headers) {
            var resp = _client.Put (url, serialize (body), headers);
            var obj = resp.Result;
            return deserialize<T> (obj);
        }

        /// <summary>
        /// Executa um requisição com método Get do protocolo http, 
        /// retornado o conteúdo JSON desserializado para o objeto c#.
        /// </summary>
        /// <param name="url">Url da requisição</param>
        /// <param name="headers">Cabeçalho para ser adicionado na requisição</param>
        /// <typeparam name="T">Tipo do objeto JSON do conteúdo da reposta http</typeparam>
        /// <returns>Objeto desserializado</returns>
        public T Get<T> (string url, params Header[] headers) {
            var resp = _client.Get (url, headers);
            var json = resp.Result;
            return deserialize<T>(json);
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

    }
}