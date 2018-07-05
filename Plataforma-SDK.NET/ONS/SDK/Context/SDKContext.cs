namespace ONS.SDK.Context
{
    public class SDKContext<T>: IContext<T> where T: IPayload
    {
        public IEvent<T> Event {get;set;}

        public IEvent GetEvent()
        {
            return Event;
        }

        public void SetEvent(IEvent value)
        {
            Event = (IEvent<T>)value;
        }
    }

    public class SDKEvent<T>: IEvent<T> where T: IPayload
    {
        public string Name {get;set;}
        
        public T Payload {get;set;}

        public IPayload GetPayload()
        {
            return Payload;
        }

        public void SetPayload(IPayload value)
        {
            Payload = (T) value;
        }
    }
}