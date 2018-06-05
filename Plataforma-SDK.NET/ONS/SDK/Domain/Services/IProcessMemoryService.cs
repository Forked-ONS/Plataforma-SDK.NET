using ONS.SDK.Domain.Core;

namespace ONS.SDK.Domain.Services {
    public interface IProcessMemoryService<T> {
        Memory<T> Head (string processInstanceId);

        void Commit(Memory<T> memory);
    }
}