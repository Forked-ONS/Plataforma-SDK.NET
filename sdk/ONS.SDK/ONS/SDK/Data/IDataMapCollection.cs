using System;
using System.Collections.Generic;

namespace ONS.SDK.Data
{
    /// <summary>
    /// Define uma coleção de mapas para serem registrados no SDK.
    /// </summary>
    public interface IDataMapCollection
    {
        /// <summary>
        /// Coleção de mapas para registro no SDK.
        /// </summary>
        IDictionary<string, Type> DataMaps {get;}
    }
}