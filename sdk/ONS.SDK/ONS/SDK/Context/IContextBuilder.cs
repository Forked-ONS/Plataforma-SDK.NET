using ONS.SDK.Worker;

namespace ONS.SDK.Context
{
    public interface IContextBuilder
    {
        IContext Build(ContextBuilderParameters parameters = null);

        IContext Build();
        
        IContext Build(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent);

    }
}