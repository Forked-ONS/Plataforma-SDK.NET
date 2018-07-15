
namespace ONS.SDK.Data.Query
{
    public interface IQueryFilter {
        
        string Name {get;}

        object GetParameters();

        void SetParameters(object parameters);
    }

    public interface IQueryFilter<T>: IQueryFilter {
        
        T Parameters {get;}
        
    }

}