using ONS.SDK.Data;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Context
{
    public interface IContext
    {
        string ProcessId {get;}
        
        string SystemId {get;}
        
        string InstanceId {get;}
        
        string EventOut {get;}
        
        bool Commit {get;}

        Memory Memory {get;}

        Memory UpdateMemory();

        IDataContext DataContext {get;}

        IEvent GetEvent();
        void SetEvent(IEvent value);
    }

    public interface IContext<T>: IContext
    {
        IEvent<T> Event {get;set;}
    }
    
}