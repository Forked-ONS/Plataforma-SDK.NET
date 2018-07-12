using System;
using System.Net;
using System.Net.Http;

namespace ONS.SDK.Worker {
    
    public class SDKHttpException : Exception {
        
        public HttpStatusCode StatusCode {get;private set;}

        public string ResponseBody {get;private set;}

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