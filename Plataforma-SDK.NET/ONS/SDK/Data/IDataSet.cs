using System.Collections;
using System.Collections.Generic;
using ONS.SDK.Data.Impl;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data
{
    public interface IDataSet: IEnumerable
    {
        IList<Model> AllEntities {get;}

        IList<Model> TrackingEntities {get;}
    }

    public interface IDataSet<T>: IDataSet, IEnumerable<T> where T: Model
    {

        string MapName {get;}

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        T FindById(string id);

        void DeleteById(string id);

    }
}