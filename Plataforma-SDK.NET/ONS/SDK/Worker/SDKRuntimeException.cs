using System;

namespace ONS.SDK.Worker {
    
    public class SDKRuntimeException : Exception {
        
        public SDKRuntimeException (string message) : base (message) { }

        public SDKRuntimeException (string message, Exception ex) : base (message, ex) { }
    }
}