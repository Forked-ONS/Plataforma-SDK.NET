using System.Collections.Generic;
using ONS.SDK.Domain.Core;

namespace ONS.SDK.Services
{
    public interface IInstalledAppService
    {
        List<InstalledApp> FindBySystemIdAndType(string systemId, string type);
    }
}