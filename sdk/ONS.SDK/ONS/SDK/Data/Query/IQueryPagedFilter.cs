
namespace ONS.SDK.Data.Query
{
    /// <summary>
    /// Define o filtro de pesquisa para consulta paginada.
    /// Adiciona os parâmetros básicos de paginação.
    /// </summary>
    public interface IQueryPagedFilter: IQueryFilter {
        
        /// <summary>
        /// Página corrente do resultado da pesquisa.
        /// </summary>
        /// <value></value>
        int Page {get;}

        /// <summary>
        /// Quantidade de registros por página.
        /// </summary>
        int PageSize {get;}
    
    }

    /// <summary>
    /// Define o filtro de pesquisa paginada e tipada para o objeto de parâmetros.
    /// </summary>
    /// <typeparam name="T">Tipo do objeto com os parâmetros de filtros da pesquisa.</typeparam>
    public interface IQueryPagedFilter<T>: IQueryPagedFilter, IQueryFilter<T> {
    
    }

}