using System;

namespace ONS.SDK.Worker {
    
    /// <summary>
    /// Define as exceções relacionadas a execução de serviços de negócio, usados para atender eventos do sistema.
    /// </summary>
    public class SDKBusinessException : Exception {
        
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        public SDKBusinessException(string message) : base (message) { }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        /// <param name="ex">Exceção original que provocou esta exceção.</param>
        public SDKBusinessException (string message, Exception ex) : base (message, ex) { }
    }
}