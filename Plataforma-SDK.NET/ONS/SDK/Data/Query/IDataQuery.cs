

using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data.Query
{
    public interface IDataQuery
    {
        IQuerySet<T> Set<T>() where T: Model;
    }

}