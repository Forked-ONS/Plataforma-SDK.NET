using System;

namespace ONS.SDK.Configuration {
    
    public class BadConfigException : Exception {
        public BadConfigException (string message) : base (message) { }
    }
}