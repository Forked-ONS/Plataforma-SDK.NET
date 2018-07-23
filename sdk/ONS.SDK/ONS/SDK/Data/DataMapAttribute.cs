using System;

namespace ONS.SDK.Data
{
    /// <summary>
    /// Representa o attribute de anotação do mapa de dados do processo.
    /// Indica qual entidade do mapa uma determinada classe está mapeando.
    /// </summary>
    public class DataMapAttribute: Attribute
    {
        /// <summary>
        /// Nome da entidaded no mapa do processo.
        /// </summary>
        public string MapName { get; private set; }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="mapName">Parâmetro do construtor.</param>
        public DataMapAttribute(string mapName) {
            MapName = mapName;
        }
    }
}