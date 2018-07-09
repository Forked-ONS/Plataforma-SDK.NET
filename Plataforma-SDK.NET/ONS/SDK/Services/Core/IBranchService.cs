using System.Collections.Generic;
using ONS.SDK.Domain.Base;
using ONS.SDK.Domain.Core;

namespace ONS.SDK.Services
{
    public interface IBranchService: ICoreService<Branch>
    {
        List<Branch> FindBySystemIdAndName(string systemId, string name);

        List<Branch> FindBySystemIdAndOwner(string systemId, string owner);
    }
}