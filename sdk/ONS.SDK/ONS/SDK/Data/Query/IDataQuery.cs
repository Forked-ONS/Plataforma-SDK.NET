

using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data.Query
{
    /// <summary>
    /// Definição de lista de tipos entidade para fazer consulta.
    /// </summary>
    public interface IDataQuery
    {
        /// <summary>
        /// Obtém lista de tipo de entidade para consulta.
        /// </summary>
        /// <typeparam name="T">Tipo de entidade para consulta</typeparam>
        /// <returns>Lista de tipo de entidade para consulta</returns>
        IQuerySet<T> Set<T>() where T: Model;
    }

}