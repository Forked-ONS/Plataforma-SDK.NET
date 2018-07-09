using ONS.SDK.Domain.Core;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Services {
    public interface IExecutorService {
        
        ProcessInstance CreateInstance(MemoryEvent mevent);
    }
}