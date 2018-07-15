
namespace ONS.SDK.Data.Query
{
    public interface IQueryPagedFilter: IQueryFilter {
        
        int Page {get;}

        int PageSize {get;}
    
    }

    public interface IQueryPagedFilter<T>: IQueryPagedFilter, IQueryFilter<T> {
    
    }

}