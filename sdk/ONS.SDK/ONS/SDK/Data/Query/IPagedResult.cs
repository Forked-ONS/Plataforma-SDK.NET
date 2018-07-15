

using System.Collections.Generic;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data.Query
{
    public interface IPagedResult<T> where T: Model {
        
        int Page {get;}

        int PageSize {get;}

        int TotalCount {get;}    

        IList<T> Result {get;}
    
    }

}