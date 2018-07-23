using System.Collections.Generic;
using ONS.SDK.Domain.Base;
using ONS.SDK.Domain.Core;

namespace ONS.SDK.Services
{
    /// <summary>
    /// Define serviços para obter e salvar informações da entidade de operação do core.
    /// </summary>
    public interface IOperationService: ICoreService<Operation>
    {
        List<Operation> FindByEventInAndSystemId(string systemId, string eventIn);
    }
}