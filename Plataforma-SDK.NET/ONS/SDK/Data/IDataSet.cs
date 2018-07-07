using System.Collections;
using System.Collections.Generic;
using ONS.SDK.Data.Impl;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data
{
    public interface IDataSet
    {
         IEnumerable<IEntityState> EntitiesStates {get;}
    }

    public interface IDataSet<T>: IDataSet, IEnumerable<T> where T: BaseEntity
    {
         void Insert(T entity);

         void Update(T entity);

         void Delete(T entity);

         IEnumerable<EntityState<T>> AllStates {get;}
    }
}