using ONS.SDK.Data.Query;

namespace ONS.SDK.Impl.Data.Query
{
    public class QueryFilter<T> : IQueryFilter<T>
    {
        public string Name {get;set;}

        public T Parameters {get;set;}

        public object GetParameters()
        {
            return Parameters;
        }

        public void SetParameters(object parameters)
        {
            Parameters = (T) parameters;
        }
    }
}