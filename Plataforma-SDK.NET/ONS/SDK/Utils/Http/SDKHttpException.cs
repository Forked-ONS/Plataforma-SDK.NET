using System;
using System.Net;
using System.Net.Http;

namespace ONS.SDK.Worker {
    
    public class SDKHttpException : Exception {
        
        public HttpStatusCode StatusCode {get;private set;}

        public string ResponseBody {get;private set;}

        public string ReasonPhrase {get;private set;}

        public SDKHttpException (string message) : base (message) { }

        public SDKHttpException (HttpResponseMessage response, string message, Exception ex) : base (message, ex) { 
            this.StatusCode = response.StatusCode;
            this.ResponseBody = response.Content.ReadAsStringAsync().Result;
            this.ReasonPhrase = response.ReasonPhrase;
        }

        public SDKHttpException (string message, Exception ex) : base (message, ex) { }
    }
}