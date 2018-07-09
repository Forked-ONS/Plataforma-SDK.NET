using ONS.SDK.Configuration;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Services;
using ONS.SDK.Services.Impl;
using Microsoft.Extensions.Logging;
using ONS.SDK.Utils.Http;
using Newtonsoft.Json;

namespace ONS.SDK.Services.Impl.ProcessMemory
{
    public class ProcessMemoryService : IProcessMemoryService 
    {
        private readonly ILogger _logger;

        private readonly ProcessMemoryConfig _config;

        private readonly HttpClient _client;
        
        private readonly JsonHttpClient _jsonClient;

        private readonly string _appOriginSDK = SDKConfiguration.AppOriginSDK;

        public ProcessMemoryService(ILogger<ProcessMemoryService> logger, 
            ProcessMemoryConfig conf, HttpClient client, JsonHttpClient jsonClient) 
        {
            this._logger = logger;
            this._config = conf;
            this._client = client;
            this._jsonClient = jsonClient;
        }

        public Memory Head(string processInstanceId) {

            var url = $"{this._config.Url}/{processInstanceId}/head?app_origin={this._appOriginSDK}";
            
            Memory retorno = null;

            var result = this._client.Get(url).Result;

            if (!string.IsNullOrEmpty(result))
            {
                retorno = JsonConvert.DeserializeObject<Memory>(result);
            }
            
            return retorno;
        }

        public void Commit(Memory memory)
        {
            var url = $"{this._config.Url}/{memory.InstanceId}/commit?app_origin={this._appOriginSDK}";

            this._jsonClient.Post<string>($"{this._config.Url}/{memory.InstanceId}/commit?app_origin={this._appOriginSDK}", memory);            
        }
    }
}