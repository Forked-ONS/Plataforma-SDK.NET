using System;
using Microsoft.Extensions.Configuration;
using ONS.SDK.Configuration;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Context {

    public class ExecutionParameter 
    {
        public MemoryEvent MemoryEvent { get; internal set; }

        public bool SynchronousPersistence { get; internal set; }

        public string EventName { 
            get {
                return MemoryEvent != null? MemoryEvent.Name : null;
            } 
        }

        public string Branch { 
            get {
                return MemoryEvent != null? MemoryEvent.Branch : null;
            } 
        }

        public DateTime? ReferenceDate { 
            get {
                return MemoryEvent != null && MemoryEvent.ReferenceDate.HasValue ? 
                    MemoryEvent.ReferenceDate.Value : default(DateTime?);
            } 
        }

        public Reprocess Reprocess {
            get {
                return MemoryEvent != null? MemoryEvent.Reprocess : null;
            }
        }

        public string InstanceId { get; internal set; }
        
        public ExecutionParameter()
        {        
        }

        public override string ToString() {
            return $"{this.GetType().Name}[EventName={EventName}, Branch={Branch}, SynchronousPersistence={SynchronousPersistence}, " + 
                $"ReferenceDate={ReferenceDate}, InstanceId={InstanceId}, Reprocess={Reprocess}]";
        }
    }
}