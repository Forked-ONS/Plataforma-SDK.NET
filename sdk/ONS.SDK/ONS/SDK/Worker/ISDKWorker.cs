using ONS.SDK.Context;

namespace ONS.SDK.Worker
{
    /// <summary>
    /// Interface que define os métodos responsáveis por executar um serviço de negócio que atende a um evento do sistema. 
    /// O evento a ser atendido pelo SDK será recebido do executor, em caso de execução assíncrona, ou da camada web em caso de execução síncrona.
    /// O payload equivale aos parâmetros que serão utilizados na execução do serviço de negócio.
    /// </summary>
    public interface ISDKWorker
    {
        /// <summary>
        /// Neste caso, não existe instância de processamento ainda, são informados os parâmetros e nome 
        /// de evento para que seja criada uma nova instância.
        /// O SDK carrega os dados de memória de processamento, e registra o evento e demais informações na plataforma.
        /// Por meio da configuração de "Bind" o SDK identifica qual o serviço responsável por atender o evento informado.
        ///
        /// Esta execução é normalmente chamada a partir da camada web, pois quando vem pelo executor da plataforma, 
        /// este já tem criado a instância.
        /// </summary>
        /// <param name="payload">Contém os parâmetros para execução do serviço de negócio.</param>
        /// <param name="eventName">Nome do evento relacionado ao serviço de negócio que atente esse evento.</param>
         void Run(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent);

        /// <summary>
        /// Neste caso, a instância é passada para ser carregada e executada pelo SDK. 
        /// A instância foi criada e repassada pelo executor, no execution context, o SDK complementa o contexto da memória de processamento, 
        /// e registra o evento e demais informações na plataforma.
        /// Por meio da configuração de "Bind" o SDK identifica qual o serviço responsável por atender o evento informado.
        /// 
        /// Esta execução é normalmente chamada a partir do executor pois a instância já existe.
        /// </summary>
        void Run();
    }
}