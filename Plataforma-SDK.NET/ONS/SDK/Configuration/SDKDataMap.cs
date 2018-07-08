using System;
using System.Collections.Generic;
using System.Linq;
using ONS.SDK.Data;
using ONS.SDK.Domain.Base;
using ONS.SDK.Worker;

namespace ONS.SDK.Configuration
{
    public class SDKDataMap
    {
        private static IDictionary<string, Type> _maps = new Dictionary<string, Type>();

        public static void BindsMap(IDictionary<string, Type> maps) {
            if (maps != null && maps.Count > 0) {
                foreach(var it in maps) {
                    _maps.Add(it);
                }
            }
        }

        public static void BindsMapCollection<T>() where T: IDataMapCollection {
            var type = typeof(T);
            
            var dataMap = (IDataMapCollection) Activator.CreateInstance(type);

            SDKDataMap.BindsMap(dataMap.DataMaps);   
        }

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

        public static Type GetMap(string typeName) 
        {
            if (string.IsNullOrEmpty(typeName)) {
                throw new SDKRuntimeException("Invalid typeName is null.");
            }
            return _maps.ContainsKey(typeName)? _maps[typeName] : null;
        }
    }
}