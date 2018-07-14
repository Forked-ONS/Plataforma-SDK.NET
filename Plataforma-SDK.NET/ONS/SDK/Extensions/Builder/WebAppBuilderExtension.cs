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
    public static class WebAppBuilderExtension
    {
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

        public static IWebHostBuilder BindEvents<T>(this IWebHostBuilder builder)
        {
            SDKConfiguration.BindEvents<T>();
            
            return builder;
        }

        public static IApplicationBuilder BindEvents<T>(this IApplicationBuilder builder)
        {
            SDKConfiguration.BindEvents<T>();
            
            return builder;
        }

        public static IWebHostBuilder BindDataMap<T>(this IWebHostBuilder builder, string mapName) where T: Model
        {
            SDKDataMap.BindMap<T>(mapName);
            
            return builder;
        }

        public static IApplicationBuilder BindDataMap<T>(this IApplicationBuilder builder, string mapName) where T: Model
        {
            SDKDataMap.BindMap<T>(mapName);
            
            return builder;
        }

        public static IWebHostBuilder BindDataMap<T>(this IWebHostBuilder builder) where T: Model
        {
            SDKDataMap.BindMap<T>();
            
            return builder;
        }

        public static IApplicationBuilder BindDataMap<T>(this IApplicationBuilder builder) where T: Model
        {
            SDKDataMap.BindMap<T>();
            
            return builder;
        }

        public static IWebHostBuilder UseDataMap<T>(
            this IWebHostBuilder builder) where T: IDataMapCollection
        {   
            SDKDataMap.BindsMapCollection<T>();
            
            return builder;
        }

        public static IApplicationBuilder UseDataMap<T>(
            this IApplicationBuilder builder) where T: IDataMapCollection
        {   
            SDKDataMap.BindsMapCollection<T>();
            
            return builder;
        }

    }
}