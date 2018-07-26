using System;
using Microsoft.Extensions.Configuration;

namespace ONS.SDK.Context {
    
    /// <summary>
    /// Representa o contexto de execução do microserviço, recebido do executor.
    /// </summary>
    public interface IExecutionContext: IDisposable
    {
        /// <summary>
        /// Indica se o sistema está executando em modo web.
        /// Ou seja, se será levantado um self-host como servidor web, 
        /// para atender requisições http.
        /// </summary>
        bool IsExecutionWeb {get;}

        /// <summary>
        /// Indica se o sistema está executando em modo web.
        /// Ou seja, se será executado com uma rotina, que após execução é finalizada.
        /// </summary>
        bool IsExecutionConsole {get;}

        /// <summary>
        /// Identificador do sistema.
        /// </summary>
        string SystemId {get;}
        
        /// <summary>
        /// Identificador do processo.
        /// </summary>
        string ProcessId {get;}
        
        /// <summary>
        /// Identificador da instância de processo.
        /// </summary>
        string ProcessInstanceId {get;}   

        /// <summary>
        /// Parâmetros de tempo de execução para uma instância de operação
        /// que atende a um evento.
        /// </summary>
        ExecutionParameter  ExecutionParameter {get;set;}

        /// <summary>
        /// Incializa os parâmetros de tempo de execução para uma instância 
        /// de operação que atende a um evento.
        /// </summary>
        /// <param name="executionParameter">Parâmetros </param>
        /// <returns>Contexto da execução.</returns>
        IExecutionContext Begin(ExecutionParameter executionParameter = null);

        /// <summary>
        /// Valida os dados de execução do processo.
        /// </summary>
        void Validate();

        /// <summary>
        /// Valida a instância de processamento para execução do process.
        /// </summary>
        void ValidateInstanceId();
    }
    
}