using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ONS.SDK.Configuration;
using ONS.SDK.Context;
using ONS.SDK.Data;
using ONS.SDK.Data.Persistence;
using ONS.SDK.Domain.Base;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Impl.Data.Query;
using ONS.SDK.Services.Domain;
using ONS.SDK.Worker;

namespace ONS.SDK.Impl.Data.Persistence
{
    public class SDKDataContextBuilder: IDataContextBuilder
    {
        private readonly ILogger<SDKDataContextBuilder> _logger;

        private readonly IDomainService _domainService;

        private readonly MethodInfo _queryByFilter;

        public SDKDataContextBuilder(ILogger<SDKDataContextBuilder> logger, IDomainService domainService) {
            this._logger = logger;
            this._domainService = domainService;
            this._queryByFilter = this._domainService.GetType().GetMethod("QueryByFilter");
        }

        public IDataContext Build(Memory memory) 
        {
            try {
                return memory.DataSet != null ? _buildFromDataSet(memory) : _loadDataSetFromMap(memory);

            } catch(Exception ex) {
                throw new SDKRuntimeException($"Error while trying to build datacontext based on processing memory.", ex);
            }
        }

        private IDataContext _loadDataSetFromMap(Memory memory) 
        {
            this._logger.LogDebug("Constructing the dataset loaded from queries from persistence domainapp.");

            var dataSetMap = new DataSetMap() { Entities = new Dictionary<string, object>() };

            IList<IDataSet> dataSets = new List<IDataSet>();

            var map = memory.Map;
            if (map != null && map.Content != null) 
            {
                var mapName = map.Name;

                foreach(var mapNameItem in map.Content) {

                    var entityName = mapNameItem.Key;
                    var mapItem = mapNameItem.Value;

                    var type = SDKDataMap.GetMap(entityName);

                    if (type != null) {

                        var entitiesSet = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
                        
                        _loadDataFromMapItem(mapName, entityName, type, mapItem, entitiesSet, memory.Event.Payload as JObject);
                        
                        var dataset = (IDataSet) Activator.CreateInstance(
                            typeof(SDKDataSet<>).MakeGenericType(type), 
                            entityName, entitiesSet
                        );
    
                        dataSets.Add(dataset);

                    } else {  
                        this._logger.LogWarning($"Unregistered mapping type[{entityName}] for map. mapName: {mapName}");
                    }

                    dataSetMap.Entities[entityName] = null;
                }
            }

            memory.DataSet = dataSetMap;

            return new SDKDataContext(dataSets, true);
        }

        private void _loadDataFromMapItem(string mapName, string entityName, 
            Type entityType, MapItem mapItem, 
            IList entitiesSet, JObject payload) 
        {
            var filters = new List<Filter>();
            if (mapItem.Filters.ContainsKey(SDKConstants.FilterNameAll)) {
                
                var filter = new Filter() {
                    Map = mapName,
                    Entity = entityName,
                    Name = SDKConstants.FilterNameAll
                };
                
                filters.Add(filter);
            }
            else {
                foreach (var itemFilter in mapItem.Filters) {

                    var filter = new Filter() {
                        Map = mapName,
                        Entity = entityName,
                        Name = itemFilter.Key
                    };

                    var filterToAdd = true;
                    if (!string.IsNullOrEmpty(itemFilter.Value)) 
                    {
                        filter.Parameters = QueryHelper
                            .MakeParameters(itemFilter.Value, payload, out filterToAdd);
                    }

                    if (filterToAdd) {
                        filters.Add(filter);
                    }
                }
            }

            if (filters.Any()) 
            {
                foreach(var filter in filters) 
                {    
                    var queryByFilterGeneric = this._queryByFilter.MakeGenericMethod(entityType);
                    var result = queryByFilterGeneric.Invoke(this._domainService, new [] {filter}) as ICollection;
                    
                    if (result != null && result.Count > 0) 
                    {
                        foreach (var item in result)
                        {
                            var modelResult = item as Model;
                            if (modelResult != null) {
                                if (!this._containsModel(entitiesSet, modelResult.Id)) {
                                    entitiesSet.Add(modelResult);
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool _containsModel(IList entitiesSet, string id) {
            var retorno = false;
            foreach (var item in entitiesSet)
            {
                if (string.Equals(((Model)item).Id, id)) {
                    retorno = true;
                    break;
                }
            }
            return retorno;
        }

        private IDataContext _buildFromDataSet(Memory memory) 
        {
            var dataSetMap = memory.DataSet;

            this._logger.LogDebug("Constructing the dataset from the data in the processing memory.");

            IList<IDataSet> dataSets = new List<IDataSet>();

            var map = memory.Map;
            if (map != null && map.Content != null) 
            {
                var mapName = map.Name;

                if (dataSetMap != null && dataSetMap.Entities != null) 
                {
                    foreach(var keyPair in dataSetMap.Entities) 
                    {       
                        var entityName = keyPair.Key;
                        var entities = keyPair.Value as JArray;

                        if (entities != null) 
                        {
                            var type = SDKDataMap.GetMap(entityName);

                            if (type != null) {

                                var entitiesSet = entities.ToObject(typeof(List<>).MakeGenericType(type));

                                var dataset = (IDataSet) Activator.CreateInstance(
                                    typeof(SDKDataSet<>).MakeGenericType(type), 
                                    entityName, entitiesSet
                                );
            
                                dataSets.Add(dataset);

                            } else {  
                                this._logger.LogWarning($"Unregistered mapping type for map name. mapName: {mapName}");
                            }
                        }

                    }
                    
                }
            }
            
            return new SDKDataContext(dataSets);
        }

    }
}