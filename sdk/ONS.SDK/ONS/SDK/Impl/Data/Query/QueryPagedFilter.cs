using ONS.SDK.Data.Query;

namespace ONS.SDK.Impl.Data.Query
{
    public class QueryPagedFilter<T> : QueryFilter<T>, IQueryPagedFilter<T>
    {
        public int Page {get;set;}

        public int PageSize {get;set;}

    }
}