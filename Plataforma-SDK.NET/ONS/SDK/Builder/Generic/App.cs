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

        public SDKExecutionContext ExecutionContext {get; private set;}

        public App(IServiceProvider serviceProvider, IConfiguration configuration, 
            SDKExecutionContext executionContext) 
        {
            ServiceProvider = serviceProvider;
            Configuration = configuration;
            
            ExecutionContext = executionContext;
            
            _logger = SDKLoggerFactory.Get<App>();
        }

        private void _validateConfigurations() 
        {
            Console.WriteLine("############ " + SDKConfiguration.Binds.Any());
            if (!SDKConfiguration.Binds.Any()) {

                _logger.LogWarning("No events have been configured to respond to platform services.");
            }
        }

        public void Run () 
        {
            _validateConfigurations();

            var sdk = ServiceProvider.GetService<SDKWorker>();
            sdk.Run(ExecutionContext.ProcessInstanceId);
        }

    }
}