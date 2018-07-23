using ONS.SDK.Domain.Core;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Services {

    /// <summary>
    /// Define os serviços do executor para o sdk.
    /// </summary>
    public interface IExecutorService {
        
        /// <summary>
        /// Cria instância do processo para execução da operação para atender o evento.
        /// </summary>
        /// <param name="mevent">Evento para criação da instância.</param>
        /// <returns>Instância do proceso criada.</returns>
        ProcessInstance CreateInstance(MemoryEvent mevent);
    }
}