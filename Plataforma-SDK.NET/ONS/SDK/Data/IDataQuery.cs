using System.Collections;
using System.Collections.Generic;
using ONS.SDK.Data.Impl;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data
{
    public interface IDataQuery
    {
        IQuerySet<T> Set<T>() where T: Model;
    }

    public interface IQuerySet<T> where T: Model {
        
        IList<T> ById(string id);

        IList<T> All();

        IList<T> Find(string filterName = null, object filter = null);

        IList<T> Find(IQueryFilter filter);

        IPagedResult<T> FindPaged(IQueryPagedFilter filter);

        int Count(string filterName = null, object filter = null);

        int Count(IQueryFilter filter);

        bool Any(string filterName = null, object filter = null);

        bool Any(IQueryFilter filter);
    }

    public interface IQueryFilter {
        
        string Name {get;}

        object Parameters {get;}
    }

    public interface IQueryPagedFilter: IQueryFilter {
        
        int Page {get;}

        int PageSize {get;}
    
    }

    public interface IPagedResult<T>: IEnumerable<T> where T: Model {
        
        int Page {get;}

        int PageSize {get;}

        int TotalCount {get;}    

        IList<T> Result {get;}
    
    }

}