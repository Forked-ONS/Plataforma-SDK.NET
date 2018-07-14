
namespace ONS.SDK.Data.Query
{
    public interface IQueryPagedFilter: IQueryFilter {
        
        int Page {get;}

        int PageSize {get;}
    
    }

}