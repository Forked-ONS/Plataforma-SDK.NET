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
using ONS.SDK.Data.Query;
using ONS.SDK.Impl.Data.Query;

namespace ONS.SDK.Extensions.DependencyInjection
{
    /// <summary>
    /// Classe que define uma extensão para registros dos serviços para injeção de dependências,
    /// que são serviços do SDK para acesso a plataforma, em uma aplicação com suporte a SDK da plataforma.
    /// Extensão para registro dos serviços do SDK para tipos de aplicações definidas no dotnet core.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Registra todos os componentes de IoC do SDK da plataforma, para serem utilizados
        /// na execução de operações de eventos.
        /// </summary>
        /// <param name="serviceCollection">Coleção de serviços para registro de injeção de dependência.</param>
        /// <returns>Própria coleção de serviços para registro de injeção de dependência.</returns>
        public static IServiceCollection UseSDK(this IServiceCollection serviceCollection) 
        {   
            serviceCollection.AddSingleton<ProcessMemoryConfig>();
            serviceCollection.AddSingleton<EventManagerConfig>();
            serviceCollection.AddSingleton<ExecutorConfig>();
            serviceCollection.AddSingleton<CoreConfig>();
            serviceCollection.AddSingleton<IExecutionContext, SDKExecutionContext>();
            serviceCollection.AddSingleton<IContextBuilder, ContextBuilder>();
            
            serviceCollection.AddSingleton<IDataContextBuilder, SDKDataContextBuilder>();
            serviceCollection.AddSingleton<IDataQuery, SDKDataQuery>();

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

        /// <summary>
        /// Indica a classe que registra uma coleção de mapeamento de entidades.
        /// </summary>
        /// <param name="serviceCollection">Coleção de serviços para registro de injeção de dependência.</param>
        /// <typeparam name="T">Tipo da classe que mantém a coleção de mapeamento de entidades do mapa.</typeparam>
        /// <returns>Própria coleção de serviços para registro de injeção de dependência.</returns>
        public static IServiceCollection UseDataMap<T>(
            this IServiceCollection serviceCollection) where T: IDataMapCollection
        {   
            SDKDataMap.BindsMapCollection<T>();
            
            return serviceCollection;
        }

        /// <summary>
        /// Realiza a configuração dos métodos definidos em uma classe para atender  
        /// eventos do sistema.
        /// Esse registro depende de configuração de evento do método, 
        /// utilizando o attribute SDKEventAttribute.
        /// </summary>
        /// <param name="serviceCollection">Coleção de serviços para registro de injeção de dependência.</param>
        /// <typeparam name="T">Tipo da classe com os métodos que respondem à eventos.</typeparam>
        /// <returns>Própria coleção de serviços para registro de injeção de dependência.</returns>
        public static IServiceCollection BindEvents<T>(this IServiceCollection serviceCollection)
        {
            SDKConfiguration.BindEvents<T>();
            
            return serviceCollection;
        }

        /// <summary>
        /// Registra uma classe como mapeamento de dados de uma entidade do mapa do serviço.
        /// </summary>
        /// <param name="serviceCollection">Coleção de serviços para registro de injeção de dependência.</param>
        /// <param name="mapName">Nome da entidade no mapa.</param>
        /// <typeparam name="T">Tipo da classe que faz mapeamento a entidade do mapa.</typeparam>
        /// <returns>Própria coleção de serviços para registro de injeção de dependência.</returns>
        public static IServiceCollection BindDataMap<T>(
            this IServiceCollection serviceCollection, string mapName) where T: Model
        {
            SDKDataMap.BindMap<T>(mapName);
            
            return serviceCollection;
        }

        /// <summary>
        /// Registra uma classe como mapeamento de dados de uma entidade do mapa do serviço.
        /// Neste caso, a classe de mapeamento deve estar decorada com o attribute DataMapAttribute.
        /// </summary>
        /// <param name="serviceCollection">Coleção de serviços para registro de injeção de dependência.</param>
        /// <typeparam name="T">Tipo da classe que faz mapeamento a entidade do mapa.</typeparam>
        /// <returns>Própria coleção de serviços para registro de injeção de dependência.</returns>
        public static IServiceCollection BindDataMap<T>(this IServiceCollection serviceCollection) where T: Model
        {
            SDKDataMap.BindMap<T>();
            
            return serviceCollection;
        }

    }
}