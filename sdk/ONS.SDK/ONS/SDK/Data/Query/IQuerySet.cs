

using System.Collections.Generic;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data.Query
{
    /// <summary>
    /// Define os métodos de pesquisa no domínio para um tipo de entidade.
    /// </summary>
    public interface IQuerySet {

        /// <summary>
        /// Obtém a quantidade de registros para uma consulta informada.
        /// </summary>
        /// <param name="filterName">Nome do filtro para pesquisa</param>
        /// <param name="filter">Objeto com os parâmetros para aplicar o filtro</param>
        /// <returns>Quantidade de registros</returns>
        int Count(string filterName = null, object filter = null);

        /// <summary>
        /// Obtém a quantidade de registros para uma consulta informada.
        /// </summary>
        /// <param name="filter">Interface de filtro de consulta</param>
        /// <returns>Quantidade de registros</returns>
        int Count(IQueryFilter filter);

        /// <summary>
        /// Indica se existem registros a serem retornados com o filtro informado.
        /// </summary>
        /// <param name="filterName">Nome do filtro para pesquisa</param>
        /// <param name="filter">Objeto com os parâmetros para aplicar o filtro</param>
        /// <returns>Se existe resultado para a consulta</returns>
        bool Any(string filterName = null, object filter = null);

        /// <summary>
        /// Indica se existem registros a serem retornados com o filtro informado.
        /// </summary>
        /// <param name="filter">Interface de filtro de consulta</param>
        /// <returns>Se existe resultado para a consulta</returns>
        bool Any(IQueryFilter filter);

    }

    /// <summary>
    /// Define os métodos de pesquisa tipada no domínio, para um tipo de entidade mapeada.
    /// </summary>
    /// <typeparam name="T">Tipo de entidade mapeada</typeparam>
    public interface IQuerySet<T>: IQuerySet where T: Model {
        
        /// <summary>
        /// Identificador da entidade para pesquisa.
        /// </summary>
        /// <param name="id">Identificador da entidade</param>
        /// <returns>Entidade encontrada para o identificador informado</returns>
        T ById(string id);

        /// <summary>
        /// Retorna todas as entidade de domínio para o tipo de entidade.
        /// </summary>
        /// <returns>Lista de todas entidade do tipo.</returns>
        IList<T> All();

        /// <summary>
        /// Consulta de entidades que atendem um filtro informado.
        /// </summary>
        /// <param name="filterName">Nome do filtro para pesquisa.</param>
        /// <param name="filter">Objeto de filtro com os parâmetros da pesquisa.</param>
        /// <returns>Lista de entidades que atendem ao filtro informado.</returns>
        IList<T> Find(string filterName = null, object filter = null);

        /// <summary>
        /// Consulta de entidades que atendem um filtro informado.
        /// </summary>
        /// <param name="filter">Dados do filtro de pesquisa.</param>
        /// <returns>Lista de entidades que atendem ao filtro informado.</returns>
        IList<T> Find(IQueryFilter filter);

        /// <summary>
        /// Consulta paginada de entidades que atendem um filtro informado.
        /// </summary>
        /// <param name="filter">Dados do filtro de pesquisa paginada.</param>
        /// <returns>Lista de entidades que atendem ao filtro informado.</returns>
        IPagedResult<T> FindPaged(IQueryPagedFilter filter);

    }

}