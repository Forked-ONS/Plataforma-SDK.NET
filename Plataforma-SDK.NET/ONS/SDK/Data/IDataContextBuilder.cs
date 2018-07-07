using ONS.SDK.Data;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Data
{
    public interface IDataContextBuilder
    {
        IDataContext Build(DataSetMap dataSetMap);

        DataSetMap ToDataSetMap(IDataContext dataContext);
    }
}