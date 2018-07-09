using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Services {
    public interface IProcessMemoryService {
        Memory Head (string processInstanceId);

        void Commit(Memory memory);
    }
}