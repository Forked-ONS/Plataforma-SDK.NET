using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONS.SDK.Configuration;
using ONS.SDK.Data;
using ONS.SDK.Domain.Base;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Worker;

namespace ONS.SDK.Data.Impl
{
    public class SDKDataContextBuilder: IDataContextBuilder
    {
        public IDataContext Build(DataSetMap dataSetMap) 
        {
            IList<IDataSet> dataSets = new List<IDataSet>();

            if (dataSetMap != null && dataSetMap.Entities != null) 
            {
                foreach(var keyPair in dataSetMap.Entities) 
                {       
                    var mapName = keyPair.Key;
                    var entities = keyPair.Value as JArray;

                    if (entities != null) 
                    {
                        var type = SDKDataMap.GetMap(mapName);

                        if (type != null) {

                            var entitiesSet = entities.ToObject(typeof(List<>).MakeGenericType(type));

                            var dataset = (IDataSet) Activator.CreateInstance(
                                typeof(SDKDataSet<>).MakeGenericType(type), 
                                mapName, entitiesSet
                            );
        
                            dataSets.Add(dataset);
                        } else {
                            // LOG como warning
                        }
                    }

                }
                
            }
            
            return new SDKDataContext(dataSets);
        }

        public DataSetMap ToDataSetMap(IDataContext dataContext) 
        {
            DataSetMap retorno = null;
            
            return retorno;
        }
    }
}