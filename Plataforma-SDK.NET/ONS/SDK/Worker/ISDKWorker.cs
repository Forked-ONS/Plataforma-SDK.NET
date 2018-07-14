using ONS.SDK.Context;

namespace ONS.SDK.Worker
{
    public interface ISDKWorker
    {
         void Run(IPayload payload, string eventName = SDKEventAttribute.DefaultEvent);

        void Run();
    }
}