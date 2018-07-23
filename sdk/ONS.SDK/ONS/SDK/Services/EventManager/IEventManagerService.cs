using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Services {
    
    /// <summary>
    /// Define serviço para salvar e envoar eventos para 
    /// o gerenciador de eventos da plataforma.
    /// </summary>
    public interface IEventManagerService {
        
        /// <summary>
        /// Envia o evento para o gerenciador de eventos.
        /// </summary>
        /// <param name="e">Evento para envio.</param>
        void Push(MemoryEvent e);

        /// <summary>
        /// Salva evento no gerenciador de eventos.
        /// </summary>
        /// <param name="e">Evento para ser salvo.</param>
        void Save(MemoryEvent e);
    }
}