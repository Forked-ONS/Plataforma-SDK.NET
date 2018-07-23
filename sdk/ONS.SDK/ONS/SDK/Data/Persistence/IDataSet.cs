using System.Collections;
using System.Collections.Generic;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data.Persistence
{
    /// <summary>
    /// Define os métodos mais básicos da lista de persistência de 
    /// um tipo de entidade mapeada.
    /// </summary>
    public interface IDataSet: IEnumerable
    {
        /// <summary>
        /// Nome do tipo da entidade no mapa do processo.
        /// </summary>
        string MapName {get;}
        
        /// <summary>
        /// Lista de todas as entidade do tipo da lista, carregadas no contexto.
        /// </summary>
        IList<Model> AllEntities {get;}

        /// <summary>
        /// Lista as entidade que sofreram mudanças no contexto do tipo de entidade informado.
        /// </summary>
        IList<Model> TrackingEntities {get;}
    }

    /// <summary>
    /// Define os métodos de persistência relacionados ao tipo da entidade, na lista tipada.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade mapeada da lista</typeparam>
    public interface IDataSet<T>: IDataSet, IEnumerable<T> where T: Model
    {
        /// <summary>
        /// Inseri a entidade no contexto para persistência.
        /// </summary>
        /// <param name="entity">Entidade para persistência.</param>
        void Insert(T entity);

        /// <summary>
        /// Atualiza a entidade no contexto para persistência.
        /// </summary>
        /// <param name="entity">Entidade para persistência.</param>
        void Update(T entity);

        /// <summary>
        /// Deleta a entidade no contexto para persistência.
        /// </summary>
        /// <param name="entity">Entidade para persistência.</param>
        void Delete(T entity);

        /// <summary>
        /// Obtém uma entidade pelo identificador.
        /// </summary>
        /// <param name="id">Identificador da entidade a qual se deseja retornar.</param>
        /// <returns>Entidade encontrada para o identificador informado.</returns>
        T FindById(string id);

        /// <summary>
        /// Deleta a entidade pelo identificador.
        /// </summary>
        /// <param name="id">Identificador da entidade.</param>
        void DeleteById(string id);

    }
}