using System;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Context
{
    public class SDKEvent<T>: IEvent<T> where T: IPayload
    {
        private readonly MemoryEvent _memoryEvent;

        public SDKEvent(MemoryEvent memoryEvent) {
            _memoryEvent = memoryEvent;
        }

        public string Name {get {return _memoryEvent.Name;}}

        public string Branch {get {return _memoryEvent.Branch;}}

        public string InstanceId {get {return _memoryEvent.InstanceId;}}

        public DateTime? ReferenceDate {get {return _memoryEvent.ReferenceDate;}}

        public string Tag {get {return _memoryEvent.Tag;}}

        public string Scope {get {return _memoryEvent.Scope;}}
        
        public Reproduction Reproduction {get {return _memoryEvent.Reproduction;}}

        public Reprocess Reprocess {get {return _memoryEvent.Reprocess;}}

        public MemoryEvent MemoryEvent { get{return _memoryEvent;} }

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