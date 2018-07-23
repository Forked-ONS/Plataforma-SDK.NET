using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Services {

    /// <summary>
    /// Define os serviços do gerenciador de memória de processamento.
    /// </summary>
    public interface IProcessMemoryService {

        /// <summary>
        /// Obtém o último registro do histórico de memória de processamento 
        /// para a instância do processo.
        /// </summary>
        /// <param name="processInstanceId">Instância do processo</param>
        /// <returns>Memória de processamento da instância do processo</returns>
        Memory Head (string processInstanceId);

        /// <summary>
        /// Salva a memória de processamento para a instância do processo.
        /// </summary>
        /// <param name="memory">Memória de processamento para persistência</param>
        void Commit(Memory memory);
    }
}