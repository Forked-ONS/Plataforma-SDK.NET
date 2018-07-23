using System;

namespace ONS.SDK.Services.Exceptions
{
    /// <summary>
    /// Representa uma exceção da validação de entidade do domínio.
    /// </summary>
    public class InvalidEntityException : Exception
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="message">Mensagem da exceção</param>
        public InvalidEntityException(string message): base(message) {

        }
    }
}