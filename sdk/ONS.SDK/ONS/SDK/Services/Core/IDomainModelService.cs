using System.Collections.Generic;
using ONS.SDK.Domain.Core;

namespace ONS.SDK.Services
{
    /// <summary>
    /// Define serviços para obter e salvar informações da entidade de modelo de domínio do core.
    /// </summary>
    public interface IDomainModelService: ICoreService<DomainModel>
    {
        List<DomainModel> FindBySystemIdAndName(string systemId, string name);
    }
}