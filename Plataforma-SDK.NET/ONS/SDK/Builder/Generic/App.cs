using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ONS.SDK.Builder;
using ONS.SDK.Configuration;
using ONS.SDK.Context;
using ONS.SDK.Log;
using ONS.SDK.Worker;

namespace ONS.SDK.Builder.Generic 
{
    public class App : IApp 
    {
        private readonly ILogger _logger;

        public IServiceProvider ServiceProvider {get; private set;}

        public IConfiguration Configuration {get; private set;}

        public App(IServiceProvider serviceProvider, IConfiguration configuration) 
        {
            this.ServiceProvider = serviceProvider;
            this.Configuration = configuration;
            
            this._logger = SDKLoggerFactory.Get<App>();
        }

        private void _validateConfigurations() 
        {
            if (SDKConfiguration.Binds.Any()) {
                if (_logger.IsEnabled(LogLevel.Debug)) {
                    var bindsStr = string.Join('/', SDKConfiguration.Binds);
                    _logger.LogDebug($"The following methods have been configured to respond to system events. Binds: {bindsStr}");
                }
            } else {
                _logger.LogWarning("No events have been configured to respond to platform services.");
            }
        }

        public void Run () 
        {
            _validateConfigurations();

            var sdk = ServiceProvider.GetService<SDKWorker>();
            sdk.Run();
        }

    }
}