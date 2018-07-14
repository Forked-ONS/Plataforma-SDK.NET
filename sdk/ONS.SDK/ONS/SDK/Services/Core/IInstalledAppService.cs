using System.Collections.Generic;
using ONS.SDK.Domain.Core;

namespace ONS.SDK.Services
{
    public interface IInstalledAppService: ICoreService<InstalledApp>
    {
        List<InstalledApp> FindBySystemIdAndType(string systemId, string type);
    }
}