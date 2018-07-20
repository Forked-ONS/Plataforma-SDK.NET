using System;

namespace ONS.SDK.Configuration {
    
    /// <summary>
    /// Representa um exceção de configuração do SDK.
    /// </summary>
    public class BadConfigException : Exception {
        public BadConfigException (string message) : base (message) { }
    }
}