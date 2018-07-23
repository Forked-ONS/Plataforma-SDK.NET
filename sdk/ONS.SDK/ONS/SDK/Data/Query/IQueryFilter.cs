
namespace ONS.SDK.Data.Query
{
    /// <summary>
    /// Define o filtro de consultas de entidades mapeadas do processo.
    /// </summary>
    public interface IQueryFilter {
        
        /// <summary>
        /// Nome do filtro da pesquisa.
        /// </summary>
        string Name {get;}

        /// <summary>
        /// Obtém o objeto com os parâmetros do filtro de pesquisa.
        /// </summary>
        /// <returns>Parâmetros da pesquisa</returns>
        object GetParameters();

        /// <summary>
        /// Seta o objeto com os parâmetros do filtro de pesquisa.
        /// </summary>
        /// <param name="parameters">Parâmetros da pesquisa</param>
        void SetParameters(object parameters);
    }

    /// <summary>
    /// Define o filtro tipado para a consulta de entidades.
    /// </summary>
    /// <typeparam name="T">Tipo do objeto com os parâmetros para pesquisa.</typeparam>
    public interface IQueryFilter<T>: IQueryFilter {
        
        /// <summary>
        /// Objeto com os parâmetros do filtro para a consulta.
        /// </summary>
        T Parameters {get;}
        
    }

}