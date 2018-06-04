using Newtonsoft.Json;
using ONS.SDK.Domain.Core;
using System.Threading.Tasks;
using ONS.SDK.Domain.Services;
namespace ONS.SDK.Platform.ProcessMemoryClient
{
    public class ProcessMemoryService<T> : IProcessMemoryService<T>
    {

        public ProcessMemoryService()
        {
        }
        public virtual Memory<T> Head(string processInstanceId)
        {
            return null;
        }

        public virtual void Commit(Context<T> context)
        {

        }

    }
}
