using ONS.SDK.Worker;

namespace ONS.SDK.Context
{
    /// <summary>
    /// Construção do contexto de execução da operação do evento.
    /// </summary>
    public interface IContextBuilder
    {
        /// <summary>
        /// Constroi o contexto com os parâmetros necessários.
        /// </summary>
        /// <param name="parameters">Parâmetros para construção do builder.</param>
        /// <returns>Contexto de execução da operação do evento.</returns>
        IContext Build(ContextBuilderParameters parameters = null);

        /// <summary>
        /// Constrói o contexto de execução da operação do evento.
        /// </summary>
        IContext Build();
        
        /// <summary>
        /// Constrói o contexto de execução da operação do evento.
        /// </summary>
        /// <param name="payload">Parâmetros para execução da operação, passado pelo evento.</param>
        /// <param name="eventName">Nome do evento recebido para executar a operação.</param>
        /// <returns>Contexto de execução da operação do evento.</returns>
        IContext Build(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent);

    }
}