using System.Collections.Generic;
using ONS.SDK.Domain.Core;

namespace ONS.SDK.Services
{
    /// <summary>
    /// Define serviços para obter e salvar informações da entidade de instalação de aplicação do core.
    /// </summary>
    public interface IInstalledAppService: ICoreService<InstalledApp>
    {
        List<InstalledApp> FindBySystemIdAndType(string systemId, string type);
    }
}