using System;
using System.Collections.Generic;
using System.Linq;
using ONS.SDK.Configuration;

namespace ONS.SDK.Data.Impl
{
    public abstract class AbstractDataMapCollection : IDataMapCollection
    {
        private static IDictionary<string, Type> _maps = new Dictionary<string, Type>();        

        private bool loaded;

        public abstract void Load();

        public void BindMap<T>(string typeName) 
        {
            var type = typeof(T);

            if (string.IsNullOrEmpty(typeName)) {
                throw new BadConfigException(
                    string.Format("Invalid typeName is null to type: {0}.",
                    type.Name)
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

        public void BindMap<T>() 
        {
            var type = typeof(T);

            var attr = (DataMapAttribute) type
                .GetCustomAttributes(typeof(DataMapAttribute), false).SingleOrDefault();

            if (attr != null) {
                var mapName = attr.MapName;
                BindMap<T>(mapName);
            } else {
                throw new BadConfigException(
                    string.Format("Not foun attribute[DataMapAttribute] of class with datamap name, type: {0}.",
                    type.FullName)
                );
            }
        }

        public IDictionary<string, Type> DataMaps {
            get {
                if (!loaded) {
                    Load();
                    loaded = true;
                }
                return _maps;
            }
        }
    }
}