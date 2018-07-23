using ONS.SDK.Data;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Data.Persistence
{
    /// <summary>
    /// Define interface de construção do contexto de persistência.
    /// </summary>
    public interface IDataContextBuilder
    {
        /// <summary>
        /// Constrói o contexto a partir da memória de processamento, 
        /// caso na memória os dados do mapa ainda não esteja carregado, serão executadas 
        /// as consultas do mapa e carregas na memória e contexto.
        /// </summary>
        /// <param name="memory">Memória de processamento.</param>
        /// <returns>Contexto de persistência construído.</returns>
        IDataContext Build(Memory memory);
    }
}