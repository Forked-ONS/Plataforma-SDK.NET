using System;
using ONS.SDK.Data;
using ONS.SDK.Data.Persistence;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Context
{
    /// <summary>
    /// Interface que representa o contexto para tratar um evento, com os dados
    /// de execução, de memória de execução e processamento.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Identificador do processo que responde ao evento.
        /// </summary>
        string ProcessId {get;}
        
        /// <summary>
        /// Identificador do sistema ao qual o processo pertence.
        /// </summary>
        string SystemId {get;}
        
        /// <summary>
        /// Identificador da instância de execução da operação que atende o evento.
        /// </summary>
        string InstanceId {get;}
        
        /// <summary>
        /// Indica o evento de saída que será gerado após a execução da operação.
        /// </summary>
        string EventOut {get;}
        
        /// <summary>
        /// Indica se será realizado commit dos dados para o domínio, 
        /// após a execução da operação.
        /// </summary>
        bool Commit {get;}

        /// <summary>
        /// Representa a memória de processamento da execução da operação.
        /// </summary>
        Memory Memory {get;}

        /// <summary>
        /// Contém as informações de criação de fork, que pode ser criado ao final 
        /// da execução da operação.
        /// </summary>
        Fork Fork {get;}

        /// <summary>
        /// Cria um fork para a linha de dados de domínio (por exemplo: master -> vsfork),
        /// O fork equivale a um branch de dados, quando se pensa em controle de versão.
        /// O fork será criado ao final da execução do processo, caso tenha sido indicado 
        /// durante a execução da operação.
        /// </summary>
        /// <param name="forkName">Nome do branch a ser criado.</param>
        /// <param name="forkDescription">Descrição do branch a ser criado.</param>
        /// <param name="startedAt">Data de inicialização do branch.</param>
        /// <returns>Retorna o Fork que indica o branch solicitado para ser criado.</returns>
        Fork CreateFork(string forkName, string forkDescription, DateTime? startedAt = null);

        /// <summary>
        /// Atualiza a memória de processamento com os dadosdo contexto.
        /// </summary>
        /// <returns>Retorna a própria memória atualizada.</returns>
        Memory UpdateMemory();

        /// <summary>
        /// Contexto de dados carregados pelo mapa, para ser mantido e 
        /// utilizado durante a execução da operação.
        /// </summary>
        IDataContext DataContext {get;}

        /// <summary>
        /// Obtém o evento do contexto.
        /// </summary>
        /// <returns>Evento do contexto.</returns>
        IEvent GetEvent();

        /// <summary>
        /// Seta o evento para o contexto.
        /// </summary>
        /// <param name="value">Evento do contexto.</param>
        void SetEvent(IEvent value);
    }

    public interface IContext<T>: IContext
    {
        IEvent<T> Event {get;set;}
    }
    
}