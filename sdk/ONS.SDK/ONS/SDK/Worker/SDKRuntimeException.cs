using System;

namespace ONS.SDK.Worker {
    
    /// <summary>
    /// Define as exceções relacionadas a erros que podem acontecer em tempo de execução do SDK, 
    /// utilizando os serviços da plataforma.
    /// </summary>
    public class SDKRuntimeException : Exception {
        
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        public SDKRuntimeException (string message) : base (message) { }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        /// <param name="ex">Exceção original que provocou esta exceção.</param>
        public SDKRuntimeException (string message, Exception ex) : base (message, ex) { }
    }
}