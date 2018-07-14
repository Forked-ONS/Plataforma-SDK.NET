

using System.Collections.Generic;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data.Query
{
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

}