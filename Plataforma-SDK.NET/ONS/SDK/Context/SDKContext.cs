using ONS.SDK.Data;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Context
{
    public class SDKContext<T>: IContext<T> where T: IPayload
    {
        private readonly Memory _memory;

        private readonly IDataContext _dataContext;

        public SDKContext(Memory memory, IDataContext dataContext = null) {
            _memory = memory;
            _dataContext = dataContext;
            Event = new SDKEvent<T>(_memory.Event);
        }

        public string ProcessId { get{return _memory.ProcessId;} }
        
        public string SystemId { get{return _memory.SystemId;} }
        
        public string InstanceId { get{return _memory.InstanceId;} }
        
        public string EventOut { get{return _memory.EventOut;} }
        
        public bool Commit { get{return _memory.Commit;} }

        public IEvent<T> Event {get;set;}

        public Memory Memory { get{return _memory;} }

        public IDataContext DataContext {get{return _dataContext;}}

        public IEvent GetEvent()
        {
            return Event;
        }

        public void SetEvent(IEvent value)
        {
            Event = (IEvent<T>)value;
        }
    }

}