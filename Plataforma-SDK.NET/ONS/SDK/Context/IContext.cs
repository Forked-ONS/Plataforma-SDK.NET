using ONS.SDK.Domain.Core;

namespace ONS.SDK.Context
{
    public interface IContext
    {
        string ProcessId {get;}
        
        string SystemId {get;}
        
        string InstanceId {get;}
        
        string EventOut {get;}
        
        bool Commit {get;}
        
        ProcessMap Map {get;}

        Memory Memory {get;}

        IEvent GetEvent();
        void SetEvent(IEvent value);
    }

    public interface IContext<T>: IContext
    {
        IEvent<T> Event {get;set;}
    }
    
}