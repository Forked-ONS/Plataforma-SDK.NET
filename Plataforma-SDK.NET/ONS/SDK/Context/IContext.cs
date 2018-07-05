namespace ONS.SDK.Context
{
    public interface IContext
    {
        IEvent GetEvent();
        void SetEvent(IEvent value);
    }

    public interface IContext<T>: IContext
    {
        IEvent<T> Event {get;set;}
    }
    
    public interface IEvent
    {
        string Name {get;set;}

        IPayload GetPayload();
        void SetPayload(IPayload value);
    }

    public interface IEvent<T>: IEvent
    {
        T Payload {get;set;}
    }
}