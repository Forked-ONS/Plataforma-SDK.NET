using System;
using System.Collections.Generic;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data.Persistence
{
    public interface IDataContext
    {
        IEnumerable<IDataSet> AllSet {get;}

        IList<Model> AllEntities {get;}

        IList<Model> TrackingEntities {get;}

        IDataSet<T> Set<T>() where T: Model;

        IDataSet Set(Type type);

        bool DataLoaded {get;}
    }
}