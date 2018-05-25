using ONS.PlataformaSDK.Domain;

namespace ONS.PlataformaSDK.ProcessApp
{
    
    public interface IExecutable
    {
        void Execute(IDomainContext domainContext, dynamic payload);
    }
}
