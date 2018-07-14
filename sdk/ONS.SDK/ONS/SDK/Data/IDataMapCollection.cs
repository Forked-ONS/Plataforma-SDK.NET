using System;
using System.Collections.Generic;

namespace ONS.SDK.Data
{
    public interface IDataMapCollection
    {
         IDictionary<string, Type> DataMaps {get;}
    }
}