using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ONS.SDK.Data.Query;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Impl.Data.Query
{
    public class PagedResult<T> : IPagedResult<T> where T : Model
    {
        public int Page {get;set;}

        public int PageSize {get;set;}

        public int TotalCount {get;set;}

        public IList<T> Result {get;set;}

    }
}