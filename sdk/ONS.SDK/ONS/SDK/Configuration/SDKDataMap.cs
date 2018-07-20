using System;
using System.Collections.Generic;
using System.Linq;
using ONS.SDK.Data;
using ONS.SDK.Domain.Base;
using ONS.SDK.Worker;

namespace ONS.SDK.Configuration
{
    /// <summary>
    /// Mantém as configurações de mapeamento de entidades do sistema e configuradas no mapa.
    /// As classes mapeadas para cada entidade do mapa de dados são registras e controladas nesta classe.
    /// </summary>
    public class SDKDataMap
    {
        private static IDictionary<string, Type> _maps = new Dictionary<string, Type>();

        /// <summary>
        /// Registra a lista de classes mapeadas para entidades do mapa para serem configuradas.
        /// </summary>
        /// <param name="maps">Lista de mapeamento de classes e entidades do mapa.</param>
        public static void BindsMap(IDictionary<string, Type> maps) {
            if (maps != null && maps.Count > 0) {
                foreach(var it in maps) {
                    _maps.Add(it);
                }
            }
        }

        /// <summary>
        /// Indica a classe que registra uma coleção de mapeamento de entidades.
        /// </summary>
        /// <typeparam name="T">Tipo da classe que mantém a coleção de mapeamento de entidades do mapa.</typeparam>
        public static void BindsMapCollection<T>() where T: IDataMapCollection {
            var type = typeof(T);
            
            var dataMap = (IDataMapCollection) Activator.CreateInstance(type);

            SDKDataMap.BindsMap(dataMap.DataMaps);   
        }

        /// <summary>
        /// Registra uma classe como mapeamento de dados de uma entidade do mapa do serviço.
        /// Neste caso, a classe de mapeamento deve estar decorada com o attribute DataMapAttribute.
        /// </summary>
        /// <typeparam name="T">Tipo da classe que faz mapeamento a entidade do mapa.</typeparam>
        public static void BindMap<T>() where T: Model
        {
            var type = typeof(T);

            var attr = (DataMapAttribute) type.GetCustomAttributes(typeof(DataMapAttribute), false).SingleOrDefault();

            if (attr != null) {
                var mapName = attr.MapName;
                BindMap<T>(mapName);
            } else {
                throw new BadConfigException(
                    string.Format("Not found attribute[DataMapAttribute] of class with datamap name, type: {0}.",
                    type.FullName)
                );
            }
        }

        /// <summary>
        /// Registra uma classe como mapeamento de dados de uma entidade do mapa do serviço.
        /// </summary>
        /// <param name="typeName">Nome da entidade no mapa.</param>
        /// <typeparam name="T">Tipo da classe que faz mapeamento a entidade do mapa.</typeparam>
        public static void BindMap<T>(string typeName) where T: Model 
        {
            var type = typeof(T);

            if (string.IsNullOrEmpty(typeName)) {
                throw new BadConfigException(
                    string.Format("Invalid typeName is null to type: {0}.",
                    type.FullName)
                );
            }
            if (_maps.ContainsKey(typeName)) {
                throw new BadConfigException(
                    string.Format("TypeName already configurated in data map, typeName:{0}, type: {1}.",
                    typeName, type.FullName)
                );
            }
            _maps[typeName] = type;
        }

        /// <summary>
        /// Obtém o tipo da classe mapeada para um tipo de entidade no mapa do serviço.
        /// </summary>
        /// <param name="typeName">Nome da entidade no mapa do serviço.</param>
        /// <returns>Tipo da classe mapeada para um tipo de entidade no mapa.</returns>
        public static Type GetMap(string typeName) 
        {
            if (string.IsNullOrEmpty(typeName)) {
                throw new SDKRuntimeException("Invalid typeName is null.");
            }
            return _maps.ContainsKey(typeName)? _maps[typeName] : null;
        }
        
    }
}