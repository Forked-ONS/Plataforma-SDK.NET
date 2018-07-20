using System;

namespace ONS.SDK.Worker {
    
    /// <summary>
    /// Define as exceções relacionadas a erros que podem acontecer em tempo de execução do SDK, 
    /// utilizando os serviços da plataforma.
    /// </summary>
    public class SDKRuntimeException : Exception {
        
        public SDKRuntimeException (string message) : base (message) { }

        public SDKRuntimeException (string message, Exception ex) : base (message, ex) { }
    }
}