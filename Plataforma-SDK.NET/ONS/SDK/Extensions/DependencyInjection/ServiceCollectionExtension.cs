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
using ONS.SDK.Services.Impl.EventManager;
using ONS.SDK.Worker;
using ONS.SDK.Data.Impl;
using ONS.SDK.Data;
using ONS.SDK.Domain.Base;
using ONS.SDK.Services.Impl.ProcessMemory;
using ONS.SDK.Services.Impl.Executor;
using ONS.SDK.Services.Impl.Core;
using ONS.SDK.Services.Impl.Domain;
using ONS.SDK.Services.Domain;

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
            serviceCollection.AddSingleton<IExecutionContext, SDKExecutionContext>();
            serviceCollection.AddSingleton<ContextBuilder>();
            
            serviceCollection.AddSingleton<IDataContextBuilder, SDKDataContextBuilder>();

            serviceCollection.AddSingleton<IProcessMemoryService, ProcessMemoryService>();
            serviceCollection.AddSingleton<IEventManagerService, EventManagerService>();
            serviceCollection.AddSingleton<IExecutorService, ExecutorService>();
            serviceCollection.AddSingleton<IProcessMemoryService, ProcessMemoryService>();

            serviceCollection.AddSingleton<IBranchService, BranchService>();
            serviceCollection.AddSingleton<IDependencyDomainService, DependencyDomainService>();
            serviceCollection.AddSingleton<IDomainModelService, DomainModelService>();
            serviceCollection.AddSingleton<IInstalledAppService, InstalledAppService>();
            serviceCollection.AddSingleton<IMapService, MapService>();
            serviceCollection.AddSingleton<IOperationInstanceService, OperationInstanceService>();
            serviceCollection.AddSingleton<IOperationService, OperationService>();
            serviceCollection.AddSingleton<IPresentationInstanceService, PresentationInstanceService>();
            serviceCollection.AddSingleton<IPresentationService, PresentationService>();
            serviceCollection.AddSingleton<IProcessInstanceService, ProcessInstanceService>();
            serviceCollection.AddSingleton<ISystemService, SystemService>();
            serviceCollection.AddSingleton<IDomainService, DomainService>();

            serviceCollection.AddTransient<HttpClient>();
            serviceCollection.AddTransient<JsonHttpClient>();

            serviceCollection.AddSingleton<SDKWorker>();

            return serviceCollection;
        }

        public static IServiceCollection UseDataMap<T>(
            this IServiceCollection serviceCollection) where T: IDataMapCollection
        {   
            SDKDataMap.BindsMapCollection<T>();
            
            return serviceCollection;
        }

        public static IServiceCollection BindEvents<T>(this IServiceCollection serviceCollection)
        {
            SDKConfiguration.BindEvents<T>();
            
            return serviceCollection;
        }

        public static IServiceCollection BindDataMap<T>(
            this IServiceCollection serviceCollection, string mapName) where T: Model
        {
            SDKDataMap.BindMap<T>(mapName);
            
            return serviceCollection;
        }

        public static IServiceCollection BindDataMap<T>(this IServiceCollection serviceCollection) where T: Model
        {
            SDKDataMap.BindMap<T>();
            
            return serviceCollection;
        }

    }
}