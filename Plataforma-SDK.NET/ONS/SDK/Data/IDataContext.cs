using System;
using System.Collections.Generic;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data
{
    public interface IDataContext
    {
        IEnumerable<IDataSet> AllSet {get;}

         IDataSet<T> Set<T>() where T: BaseEntity;

         IDataSet Set(Type type);
    }
}