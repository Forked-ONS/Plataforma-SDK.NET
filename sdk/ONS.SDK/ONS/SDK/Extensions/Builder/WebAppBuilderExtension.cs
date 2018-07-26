using System;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ONS.SDK.Configuration;
using ONS.SDK.Worker;
using Microsoft.AspNetCore.Hosting;
using ONS.SDK.Context;
using ONS.SDK.Log;
using Microsoft.Extensions.Logging;
using ONS.SDK.Data;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Extensions.Builder
{
    /// <summary>
    /// Classe que define métodos de extensão para web application no padrão dotnet core, 
    /// com recursos para configuração e execução com suporte ao SDK da plataforma.
    /// </summary>
    public static class WebAppBuilderExtension
    {
        /// <summary>
        /// Cria a aplicação que está sendo construído pelo construtor, 
        /// e executa o SDK conforme parâmetros e configurações.
        /// </summary>
        /// <param name="builder">Construtor da aplicação</param>
        /// <returns>Própria aplicação construída durante a execução do SDK</returns>
        public static IWebHost RunSDK(this IWebHostBuilder builder) 
        {
            var app = builder.Build();

            SDKLoggerFactory.LoggerFactory = app.Services.GetService<ILoggerFactory>();

            SDKConfiguration.ServiceProvider = app.Services;

            var execContext = app.Services.GetService<IExecutionContext>();
                        
            if (execContext.IsExecutionConsole) {
                
                var sdk = app.Services.GetService<ISDKWorker>();
                sdk.Run();
                
            } else {
                app.Run();
            }

            return app;
        }

        /// <summary>
        /// Realiza a configuração dos métodos definidos em uma classe para atender  
        /// eventos do sistema.
        /// Esse registro depende de configuração de evento do método, 
        /// utilizando o attribute SDKEventAttribute.
        /// </summary>
        /// <param name="builder">Construtor da aplicação web</param>
        /// <typeparam name="T">Tipo da classe com os métodos que respondem à eventos</typeparam>
        /// <returns>Próprio construtor da aplicação</returns>   
        public static IWebHostBuilder BindEvents<T>(this IWebHostBuilder builder)
        {
            SDKConfiguration.BindEvents<T>();
            
            return builder;
        }

        /// <summary>
        /// Realiza a configuração dos métodos definidos em uma classe para atender  
        /// eventos do sistema.
        /// Esse registro depende de configuração de evento do método, 
        /// utilizando o attribute SDKEventAttribute.
        /// </summary>
        /// <param name="builder">Construtor da aplicação</param>
        /// <typeparam name="T">Tipo da classe com os métodos que respondem à eventos</typeparam>
        /// <returns>Próprio construtor da aplicação</returns>   
        public static IApplicationBuilder BindEvents<T>(this IApplicationBuilder builder)
        {
            SDKConfiguration.BindEvents<T>();
            
            return builder;
        }

        /// <summary>
        /// Registra uma classe como mapeamento de dados de uma entidade do mapa do serviço.
        /// </summary>
        /// <param name="builder">Construtor da aplicação web</param>
        /// <param name="mapName">Nome da entidade no mapa</param>
        /// <typeparam name="T">Tipo da classe que faz mapeamento a entidade do mapa</typeparam>
        /// <returns>Próprio construtor da aplicação</returns>   
        public static IWebHostBuilder BindDataMap<T>(this IWebHostBuilder builder, string mapName) where T: Model
        {
            SDKDataMap.BindMap<T>(mapName);
            
            return builder;
        }

        /// <summary>
        /// Registra uma classe como mapeamento de dados de uma entidade do mapa do serviço.
        /// </summary>
        /// <param name="builder">Construtor da aplicação</param>
        /// <param name="mapName">Nome da entidade no mapa</param>
        /// <typeparam name="T">Tipo da classe que faz mapeamento a entidade do mapa</typeparam>
        /// <returns>Próprio construtor da aplicação</returns>   
        public static IApplicationBuilder BindDataMap<T>(this IApplicationBuilder builder, string mapName) where T: Model
        {
            SDKDataMap.BindMap<T>(mapName);
            
            return builder;
        }

        /// <summary>
        /// Registra uma classe como mapeamento de dados de uma entidade do mapa do serviço.
        /// Neste caso, a classe de mapeamento deve estar decorada com o attribute DataMapAttribute.
        /// </summary>
        /// <param name="builder">Construtor da aplicação web</param>
        /// <typeparam name="T">Tipo da classe que faz mapeamento a entidade do mapa.</typeparam>
        /// <returns>Próprio construtor da aplicação</returns>   
        public static IWebHostBuilder BindDataMap<T>(this IWebHostBuilder builder) where T: Model
        {
            SDKDataMap.BindMap<T>();
            
            return builder;
        }

        /// <summary>
        /// Registra uma classe como mapeamento de dados de uma entidade do mapa do serviço.
        /// Neste caso, a classe de mapeamento deve estar decorada com o attribute DataMapAttribute.
        /// </summary>
        /// <param name="builder">Construtor da aplicação</param>
        /// <typeparam name="T">Tipo da classe que faz mapeamento a entidade do mapa.</typeparam>
        /// <returns>Próprio construtor da aplicação</returns>   
        public static IApplicationBuilder BindDataMap<T>(this IApplicationBuilder builder) where T: Model
        {
            SDKDataMap.BindMap<T>();
            
            return builder;
        }

        /// <summary>
        /// Indica a classe que registra uma coleção de mapeamento de entidades.
        /// </summary>
        /// <param name="builder">Construtor da aplicação web</param>
        /// <typeparam name="T">Tipo da classe que mantém a coleção de mapeamento de entidades do mapa.</typeparam>
        /// <returns>Próprio construtor da aplicação</returns>   
        public static IWebHostBuilder UseDataMap<T>(
            this IWebHostBuilder builder) where T: IDataMapCollection
        {   
            SDKDataMap.BindsMapCollection<T>();
            
            return builder;
        }

        /// <summary>
        /// Indica a classe que registra uma coleção de mapeamento de entidades.
        /// </summary>
        /// <param name="builder">Construtor da aplicação</param>
        /// <typeparam name="T">Tipo da classe que mantém a coleção de mapeamento de entidades do mapa.</typeparam>
        /// <returns>Próprio construtor da aplicação</returns>   
        public static IApplicationBuilder UseDataMap<T>(
            this IApplicationBuilder builder) where T: IDataMapCollection
        {   
            SDKDataMap.BindsMapCollection<T>();
            
            return builder;
        }

    }
}