using System;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Context
{
    /// <summary>
    /// Representa um evento de negócio do sistema.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Indica o nome de identificação do evento.
        /// </summary>
        string Name {get;}

        /// <summary>
        /// Identificador da instância do processo para o evento.
        /// </summary>
        /// <value></value>
        string InstanceId {get;}

        /// <summary>
        /// Data de referência no tempo, para ser considerada na execução do evento pela operação.
        /// Essa data será considerada no carregamento dos mapas e persistência dos dados, 
        /// bem como na execução geral do sistema. 
        /// No caso da data de referência ser uma data anterior a atual, o sistema utilizará 
        /// essa data para executar como se estivesse mesmo na data anterior.
        /// </summary>
        DateTime? ReferenceDate {get;}

        /// <summary>
        /// Indica a linha de dados ao qual o evento deve tratar.
        /// </summary>
        string Branch {get;}

        /// <summary>
        /// Indica a tag do evento.
        /// </summary>
        string Tag {get;}

        /// <summary>
        /// Escopo de execução do evento.
        /// </summary>
        string Scope {get;}
        
        /// <summary>
        /// Dados da reprodução, caso o evento seja um evento com intuito de realizar reprodução
        /// da execução de uma operação.
        /// </summary>
        Reproduction Reproduction {get;}

        /// <summary>
        /// Dados de reprocessamento, caso o evento seja um evento com intuito de realizar reprocessamento
        /// da execução de uma operação.
        /// </summary>
        Reprocess Reprocess {get;}

        /// <summary>
        /// Dados do evento da memória de processamento.
        /// </summary>
        /// <value></value>
        MemoryEvent MemoryEvent {get;}

        /// <summary>
        /// Obtém os dados de parâmetros do evento.
        /// </summary>
        /// <returns>Dados de parâmetros do evento</returns>
        IPayload GetPayload();
        
        /// <summary>
        /// Indica os dados de parâmetros do evento.
        /// </summary>
        /// <param name="value">Dados de parâmetros do evento</param>
        void SetPayload(IPayload value);
    }

    /// <summary>
    /// Representa um evento do sistema com 
    /// </summary>
    /// <typeparam name="T">Tipo da classe para </typeparam>
    public interface IEvent<T>: IEvent
    {
        /// <summary>
        /// Dados de parâmetros do evento.
        /// </summary>
        T Payload {get;set;}
    }
}