using System;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ONS.SDK.Context;
using ONS.SDK.Configuration;
using ONS.SDK.Utils.Http;
using ONS.SDK.Services;
using ONS.SDK.Services.Impl.ProcessMemory.ProcessMemoryClient;
using ONS.SDK.Services.Impl.EventManager;
using ONS.SDK.Worker;

namespace ONS.SDK.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection UseSDK(this IServiceCollection serviceCollection) 
        {   
            serviceCollection.AddSingleton<ProcessMemoryConfig>();
            serviceCollection.AddSingleton<EventManagerConfig>();
            serviceCollection.AddSingleton<ExecutorConfig>();
            serviceCollection.AddSingleton<CoreConfig>();
            serviceCollection.AddSingleton<SDKExecutionContext>();
            serviceCollection.AddSingleton<ContextBuilder>();

            serviceCollection.AddSingleton<IProcessMemoryService, ProcessMemoryService>();
            serviceCollection.AddSingleton<IEventManagerService, EventManagerService>();

            serviceCollection.AddTransient<HttpClient>();
            serviceCollection.AddTransient<JsonHttpClient>();

            serviceCollection.AddSingleton<SDKWorker>();

            return serviceCollection;
        }
        
    }
}