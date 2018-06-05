using ONS.SDK.Domain.Core;
using ONS.SDK.Domain.Services;
using ONS.SDK.Infra;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.ProcessMemoryClient
{
    public class ProcessMemoryService<T> : IProcessMemoryService<T> {

        private readonly ProcessMemoryConfig _config;
        private readonly JsonHttpClient _client;
        public ProcessMemoryService (ProcessMemoryConfig conf, JsonHttpClient client) {
            this._config = conf;
            this._client = client;
        }
        public virtual Memory<T> Head (string processInstanceId) {
            return this._client.Get<Memory<T>>($"{this._config.Url}/{processInstanceId}/head?app_origin=sdk_dotnet");
        }

        public void Commit(Memory<T> memory)
        {
            this._client.Post<string>($"{this._config.Url}/{memory.InstanceId}/commit?app_origin=sdk_dotnet", memory);
        }
    }
}