using System;
using System.Collections.Generic;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data.Persistence
{
    /// <summary>
    /// Interface de acesso a coleções de dados para persistência.
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// Lista de todos os tipos de entidades mapeadas e carregdas no contexto.
        /// </summary>
        IEnumerable<IDataSet> AllSet {get;}

        /// <summary>
        /// Lista todas as entidades do contexto, independente do tipo.
        /// </summary>
        IList<Model> AllEntities {get;}

        /// <summary>
        /// Lista de todas as entidades que sofreram alguma mudança, durante a execução do processo.
        /// Mudanças de inclusão, alteração ou exclusão.
        /// </summary>
        IList<Model> TrackingEntities {get;}

        /// <summary>
        /// Lista de entidades de um determinado tipo, que estão do contexto.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade no mapeada.</typeparam>
        /// <returns>Lista de entidade do tipo especificado.</returns>
        IDataSet<T> Set<T>() where T: Model;

        /// <summary>
        /// Lista de entidades de um determinado tipo, que estão do contexto.
        /// </summary>
        /// <param name="type">Tipo da entidade no mapeada.</param>
        /// <returns>Lista de entidade do tipo especificado.</returns>
        IDataSet Set(Type type);

        /// <summary>
        /// Obtem informação se o contexto foi carregado com os dados do mapa.
        /// </summary>
        bool DataLoaded {get;}
    }
}