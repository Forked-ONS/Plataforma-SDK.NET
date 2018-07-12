using System;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Context
{
    public interface IEvent
    {
        string Name {get;}

        string InstanceId {get;}

        DateTime? ReferenceDate {get;}

        string Branch {get;}

        string Tag {get;}

        string Scope {get;}
        
        Reproduction Reproduction {get;}

        Reprocess Reprocess {get;}

        MemoryEvent MemoryEvent {get;}

        IPayload GetPayload();
        void SetPayload(IPayload value);
    }

    public interface IEvent<T>: IEvent
    {
        T Payload {get;set;}
    }
}