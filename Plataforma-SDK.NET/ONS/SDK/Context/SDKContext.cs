using ONS.SDK.Domain.Core;

namespace ONS.SDK.Context
{
    public class SDKContext<T>: IContext<T> where T: IPayload
    {
        private readonly Memory _memory;

        public SDKContext(Memory memory) {
            _memory = memory;
            Event = new SDKEvent<T>(_memory.Event);
        }

        public string ProcessId { get{return _memory.ProcessId;} }
        
        public string SystemId { get{return _memory.SystemId;} }
        
        public string InstanceId { get{return _memory.InstanceId;} }
        
        public string EventOut { get{return _memory.EventOut;} }
        
        public bool Commit { get{return _memory.Commit;} }
        
        public ProcessMap Map { get{return _memory.Map;} }

        public IEvent<T> Event {get;set;}

        public Memory Memory { get{return _memory;} }

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