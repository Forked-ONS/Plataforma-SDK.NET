using System.Collections.Generic;
using ONS.SDK.Domain.Core;

namespace ONS.SDK.Services
{
    public interface IDomainModelService: ICoreService<DomainModel>
    {
        List<DomainModel> FindBySystemIdAndName(string systemId, string name);
    }
}