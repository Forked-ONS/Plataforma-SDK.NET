using System;
using System.Net;
using System.Net.Http;

namespace ONS.SDK.Worker {
    
    /// <summary>
    /// Representa exceção de requisição de http realizada pelo SDK da plataforma.
    /// </summary>
    public class SDKHttpException : Exception {
        
        /// <summary>
        /// Código de status da resposta à requisição Http.
        /// </summary>
        public HttpStatusCode StatusCode {get;private set;}

        /// <summary>
        /// Conteúdo do corpo da resposta à requisição Http.
        /// </summary>
        public string ResponseBody {get;private set;}

        /// <summary>
        /// Razão com relação ao erro de resposta à requisição Http. 
        /// </summary>
        public string ReasonPhrase {get;private set;}

        public SDKHttpException (HttpStatusCode statusCode, string responseBody, string reason) {
            this.StatusCode = statusCode;
            this.ResponseBody = responseBody;
            this.ReasonPhrase = reason;
         }

        public SDKHttpException(HttpStatusCode statusCode, string responseBody, string reason, string message) : base (message) {
            this.StatusCode = statusCode;
            this.ResponseBody = responseBody;
            this.ReasonPhrase = reason;
         }

        public SDKHttpException (HttpStatusCode statusCode, string responseBody, string reason, string message, Exception ex) : base (message, ex) { 
            this.StatusCode = statusCode;
            this.ResponseBody = responseBody;
            this.ReasonPhrase = reason;
        }
    }
}