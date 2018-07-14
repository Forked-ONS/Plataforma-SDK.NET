using System.Collections.Generic;
using ONS.SDK.Domain.Base;
using ONS.SDK.Domain.Core;

namespace ONS.SDK.Services
{
    public interface IOperationService: ICoreService<Operation>
    {
        List<Operation> FindByEventInAndSystemId(string systemId, string eventIn);
    }
}