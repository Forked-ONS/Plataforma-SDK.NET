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
                    var entities = keyPair.Value;

                    if (entities != null && entities.Any()) 
                    {
                        var type = SDKDataMap.GetMap(mapName);
                        
                        var typeEntityState = typeof(EntityState<>).MakeGenericType(type);

                        var entitiesSet = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(typeEntityState));
                        
                        foreach(var objEntity in entities) 
                        {
                            var jsonObj = objEntity as JObject;

                            if (jsonObj != null) 
                            {                                
                                var jmetadata = jsonObj.GetValue("_metadata");
                                var metadata = jmetadata.ToObject<Metadata>();
                                var entity = jsonObj.ToObject(type);

                                var entityState = Activator.CreateInstance(typeEntityState, entity, metadata);

                                entitiesSet.Add(entityState);
                            }
                        }

                        var dataset = (IDataSet) Activator.CreateInstance(
                            typeof(SDKDataSet<>).MakeGenericType(type), 
                            mapName, entitiesSet
                        );
    
                        dataSets.Add(dataset);
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