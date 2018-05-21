using System.Collections.Generic;

namespace ONS.PlataformaSDK.Domain
{
    public interface IDomainContext
    {
        List<BaseEntity> GetPersistList();
    }
}