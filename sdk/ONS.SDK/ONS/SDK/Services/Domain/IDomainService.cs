using System.Collections.Generic;
using System.Linq;
using ONS.SDK.Context;
using ONS.SDK.Domain.Base;
using ONS.SDK.Domain.Core;
using ONS.SDK.Services;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Services.Domain {

    /// <summary>
    /// Define serviços persistência e consulta na api de domínio.
    /// </summary>
    public interface IDomainService {
        
        /// <summary>
        /// Pesquisa de entidade do domínio por identificador.
        /// </summary>
        /// <param name="map">Mapa ao qual pertence a entidade da api de domínio</param>
        /// <param name="type">Tipo da entidade da api de domínio</param>
        /// <param name="id">Identificador da entidade</param>
        /// <typeparam name="T">Tipo da entidade</typeparam>
        /// <returns>Entidade retornada para o identificador informado</returns>
        T FindById<T>(string map, string type, string id) where T: Model;

        /// <summary>
        /// Histórico da entidade.
        /// </summary>
        /// <param name="map">Mapa ao qual pertence a entidade da api de domínio</param>
        /// <param name="type">Tipo da entidade da api de domínio</param>
        /// <param name="id">Identificador da entidade</param>
        /// <typeparam name="T">Tipo da entidade</typeparam>
        /// <returns>Lista de histórico de entidades</returns>
        IList<T> History<T>(string map, string type, string id) where T: Model;

        /// <summary>
        /// Pesquisa da entidade na api de domínio pelo filtro e mapa informados.
        /// </summary>
        /// <param name="map">Mapa ao qual pertence a entidade da api de domínio</param>
        /// <param name="type">Tipo da entidade da api de domínio</param>
        /// <param name="filterName">Nome do filtro para pesquisa</param>
        /// <param name="filters">Parâmetros para aplicação de filtro de pesquisa</param>
        /// <typeparam name="T">Tipo da entidade pesquisada</typeparam>
        /// <returns>Lista de entidades retornadas para o filtro de pesquisa informado</returns>
        List<T> Query<T>(string map, string type, string filterName = null, 
            IDictionary<string, object> filters = null) where T: Model;

        /// <summary>
        /// Pesquisa da entidade na api de domínio pelo filtro informado.
        /// </summary>
        /// <param name="filter">Dados de filtro da pesquisa</param>
        /// <typeparam name="T">Tipo da entidade pesquisada</typeparam>
        /// <returns>Lista de entidades retornadas para o filtro de pesquisa informado</returns>
        List<T> QueryByFilter<T>(Filter filter) where T: Model;

        /// <summary>
        /// Persiste os dados de entidades informados.
        /// </summary>
        /// <param name="map">Mapa ao qual pertence a entidade da api de domínio</param>
        /// <param name="entities">Lista de entidade para persistência</param>
        void Persist(string map, IList<Model> entities);
        
    }
}