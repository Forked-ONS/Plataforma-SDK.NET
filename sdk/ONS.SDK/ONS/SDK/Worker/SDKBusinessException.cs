using System;

namespace ONS.SDK.Worker {
    
    public class SDKBusinessException : Exception {
        
        public SDKBusinessException(string message) : base (message) { }

        public SDKBusinessException (string message, Exception ex) : base (message, ex) { }
    }
}