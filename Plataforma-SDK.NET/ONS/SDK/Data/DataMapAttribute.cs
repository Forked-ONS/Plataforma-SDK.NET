using System;

namespace ONS.SDK.Data
{
    public class DataMapAttribute: Attribute
    {
        public string MapName { get; private set; }

        public DataMapAttribute(string mapName) {
            MapName = mapName;
        }
    }
}