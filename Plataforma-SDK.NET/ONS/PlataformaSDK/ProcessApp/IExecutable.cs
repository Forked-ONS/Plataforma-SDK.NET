using ONS.PlataformaSDK.Domain;
using ONS.PlataformaSDK.Entities;

namespace ONS.PlataformaSDK.ProcessApp
{
    
    public interface IExecutable
    {
        void Execute(IDomainContext domainContext, Context context);
    }
}
