

using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using ONS.SDK.Configuration;
using ONS.SDK.Context;
using ONS.SDK.Data.Persistence;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Log;
using ONS.SDK.Worker;

namespace ONS.SDK.Impl.Context
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

        public Fork Fork {get { return _memory.Fork; }}

        public Fork CreateFork(string forkName, string forkDescription, DateTime? startedAt = null) 
        {
            if (string.IsNullOrEmpty(forkName)) {
                throw new SDKRuntimeException($"Fork name is required. InstanceId={InstanceId}");
            }
            if (string.Equals(forkName, Event.Branch)) {
                throw new SDKRuntimeException($"Fork name is equals branch of current context. Fork.Name={forkName}, InstanceId={InstanceId}");
            }

            this._memory.Fork = new Fork() {
                Name = forkName,
                Description = forkDescription,
                StartedAt = startedAt
            };
            
            return this._memory.Fork;
        }

        public IEvent GetEvent()
        {
            return Event;
        }

        public void SetEvent(IEvent value)
        {
            Event = (IEvent<T>)value;
        }

        public Memory UpdateMemory() 
        {
            this.Memory.Event.Payload = this.GetEvent().GetPayload();
            foreach (var keyPair in this.Memory.DataSet.Entities.ToList()) 
            {
                var mapName = keyPair.Key;
                var typeEntity = SDKDataMap.GetMap(mapName);
                if (typeEntity != null) {
                    var setEntities = this.DataContext.Set(typeEntity);
                    this.Memory.DataSet.Entities[mapName] = setEntities.AllEntities;
                } else {
                    var logger = SDKLoggerFactory.Get<SDKContext<T>>();
                    logger.LogWarning($"Not found type corresponding to map name: {mapName}.");
                }
            }
            return this.Memory;
        }
    }

}