using ONS.SDK.Data;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Data.Persistence
{
    public interface IDataContextBuilder
    {
        IDataContext Build(Memory memory);
    }
}