using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Services {
    
    public interface IEventManagerService {
        void Push(MemoryEvent e);
    }
}