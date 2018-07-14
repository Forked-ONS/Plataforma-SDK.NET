using System.Collections;
using System.Collections.Generic;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data.Persistence
{
    public interface IDataSet: IEnumerable
    {
        string MapName {get;}
        
        IList<Model> AllEntities {get;}

        IList<Model> TrackingEntities {get;}
    }

    public interface IDataSet<T>: IDataSet, IEnumerable<T> where T: Model
    {
        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        T FindById(string id);

        void DeleteById(string id);

    }
}