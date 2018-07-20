using System;

namespace ONS.SDK.Worker {
    
    /// <summary>
    /// Define as exceções relacionadas a execução de serviços de negócio, usados para atender eventos do sistema.
    /// </summary>
    public class SDKBusinessException : Exception {
        
        public SDKBusinessException(string message) : base (message) { }

        public SDKBusinessException (string message, Exception ex) : base (message, ex) { }
    }
}