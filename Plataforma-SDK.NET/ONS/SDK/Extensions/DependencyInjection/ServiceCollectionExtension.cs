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
using ONS.SDK.Worker;
using ONS.SDK.Data;
using ONS.SDK.Domain.Base;
using ONS.SDK.Services.Domain;
using ONS.SDK.Data.Persistence;
using ONS.SDK.Impl.Worker;
using ONS.SDK.Impl.Data.Persistence;
using ONS.SDK.Impl.Context;
using ONS.SDK.Impl.Services.ProcessMemory;
using ONS.SDK.Impl.Services.EventManager;
using ONS.SDK.Impl.Services.Executor;
using ONS.SDK.Impl.Services.Core;
using ONS.SDK.Impl.Services.Domain;

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
            serviceCollection.AddSingleton<IContextBuilder, ContextBuilder>();
            
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

            serviceCollection.AddSingleton<ISDKWorker, SDKWorker>();
            serviceCollection.AddSingleton<WorkerEvent>();
            
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