using ONS.SDK.Domain.Core;

namespace ONS.SDK.Domain.Services {
    public interface IEventManagerService {
        void Push<T> (Event<T> e);
    }
}