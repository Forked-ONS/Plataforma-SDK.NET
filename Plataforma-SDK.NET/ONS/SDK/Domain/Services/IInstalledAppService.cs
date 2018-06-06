using System.Collections.Generic;
using ONS.SDK.Domain.Core;

namespace Plataforma_SDK.NET.ONS.SDK.Domain.Services
{
    public interface IInstalledAppService
    {
        List<InstalledApp> FindBySystemIdAndType(string systemId, string type);
    }
}