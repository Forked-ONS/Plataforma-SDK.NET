using System;
using System.Collections.Generic;
using System.Linq;
using ONS.SDK.Data;
using ONS.SDK.Domain.Base;
using ONS.SDK.Worker;

namespace ONS.SDK.Data.Impl
{
    public class SDKDataContext : IDataContext
    {
        private readonly IDictionary<Type, IDataSet> _dataSets = new Dictionary<Type, IDataSet>();

        public SDKDataContext(IEnumerable<IDataSet> dataSets) 
        {
            if (dataSets == null) {
                throw new SDKRuntimeException("DataSets list is null.");
            }
            foreach(var it in dataSets) {
                var type = it.GetType().GenericTypeArguments.FirstOrDefault();
                if (type == null) {
                    throw new SDKRuntimeException("DataSet have to typed generic.");
                }
                _dataSets[type] = it;
            }
        }

        public IEnumerable<IDataSet> AllSet {
            get {
                return _dataSets.Values;
            }
        }

        public IDataSet<T> Set<T>() where T: BaseEntity {
            return (IDataSet<T>) Set(typeof(T));
        }

        public IDataSet Set(Type type) {
            if (!_dataSets.ContainsKey(type)) {
                throw new SDKRuntimeException(
                    string.Format("Data type does not exist in the data context", type));
            }
            return _dataSets[type];
        }
    }
}