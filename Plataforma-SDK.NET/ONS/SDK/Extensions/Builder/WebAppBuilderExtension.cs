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

namespace ONS.SDK.Extensions.Builder
{
    public static class WebAppBuilderExtension
    {
        public static IWebHost RunSDK(this IWebHostBuilder builder) 
        {
            var app = builder.Build();

            SDKLoggerFactory.LoggerFactory = app.Services.GetService<ILoggerFactory>();

            SDKConfiguration.ServiceProvider = app.Services;

            var execContext = app.Services.GetService<SDKExecutionContext>();
            
            if (!string.IsNullOrEmpty(execContext.ProcessInstanceId)) {
                
                var sdk = app.Services.GetService<SDKWorker>();

                sdk.Run(execContext.ProcessInstanceId);
                
            } else {
                app.Run();
            }

            return app;
        }

        public static IWebHostBuilder AddSDKBind<T>(this IWebHostBuilder builder)
        {
            SDKConfiguration.Bind<T>();
            
            return builder;
        }

        public static IApplicationBuilder AddSDKBind<T>(this IApplicationBuilder builder)
        {
            SDKConfiguration.Bind<T>();
            
            return builder;
        }
        
    }
}