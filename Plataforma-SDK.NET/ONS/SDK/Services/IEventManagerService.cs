using ONS.SDK.Domain.Core;

namespace ONS.SDK.Services {
    
    public interface IEventManagerService {
        void Push(MemoryEvent e);
    }
}