using ONS.SDK.Configuration;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Services;
using Microsoft.Extensions.Logging;
using ONS.SDK.Utils.Http;
using Newtonsoft.Json;
using ONS.SDK.Worker;
using ONS.SDK.Domain.Core;

namespace ONS.SDK.Impl.Services.Executor
{
    public class ExecutorService : IExecutorService 
    {
        private readonly ILogger _logger;

        private readonly ExecutorConfig _config;
        
        private readonly JsonHttpClient _jsonClient;

        private readonly string _urlCreate;

        public ExecutorService(ILogger<ExecutorService> logger, 
            ExecutorConfig config, JsonHttpClient jsonClient) 
        {
            this._logger = logger;
            this._config = config;
            this._jsonClient = jsonClient;

            this._urlCreate = $"{this._config.Url}/instance/create";
        }

        public ProcessInstance CreateInstance(MemoryEvent mevent) 
        {
            if(string.IsNullOrEmpty(mevent.Name)) {
                throw new SDKRuntimeException("Event name is required");
            }
            if(mevent.Payload == null){
                throw new SDKRuntimeException("Event payload is required");
            }
            /* TODO rever essa lógica
            if (this.scope){
                event.scope = this.scope;
            }*/

            return _jsonClient.Post<ProcessInstance>(_urlCreate, mevent);
        }

    }
}