

using System.Collections.Generic;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data.Query
{
    /// <summary>
    /// Define os métodos de paginação para resultados de consultas paginadas.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade da lista.</typeparam>
    public interface IPagedResult<T> where T: Model {
        
        /// <summary>
        /// Número da página corrente da paginação.
        /// </summary>
        int Page {get;}

        /// <summary>
        /// Quantidade de entidades de uma página.
        /// </summary>
        int PageSize {get;}

        /// <summary>
        /// Quantidade total de registros resultado da consulta paginada.
        /// </summary>
        int TotalCount {get;}    

        /// <summary>
        /// Resultado da consulta paginada.
        /// </summary>
        IList<T> Result {get;}
    
    }

}