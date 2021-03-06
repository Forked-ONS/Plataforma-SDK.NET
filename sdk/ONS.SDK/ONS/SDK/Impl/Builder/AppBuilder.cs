

using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ONS.SDK.Builder;
using ONS.SDK.Configuration;
using ONS.SDK.Data;
using ONS.SDK.Domain.Base;
using ONS.SDK.Extensions.DependencyInjection;
using ONS.SDK.Logger;

namespace ONS.SDK.Impl.Builder
{
    
    public class AppBuilder : IAppBuilder 
    {

        //Cria um builder com configurações default
        private IApp _app;

        private IServiceCollection _services;

        private IConfiguration _configuration;

        private IStartup _startup;

        public AppBuilder () {
            
            this._services = new ServiceCollection();
            
            this._configuration = _buildConfiguration();
            
            this._services.AddSingleton<IApp, App>();
            
            _addServicesGeneric();
        }

        private void _addServicesGeneric() {
            this._services.AddSingleton<IConfiguration>(this._configuration);
            this._services.AddLogging();
            this._services.UseSDK();
        }

        public static IAppBuilder CreateDefaultBuilder (string[] args) {
            var builder = new AppBuilder ();
            return builder;
        }

        private IConfiguration _buildConfiguration() {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

        private IServiceProvider _buildServiceProvider() {
            return this._services.BuildServiceProvider();
        }

        public IApp Build () {
            
            var serviceProvider = _buildServiceProvider();

            this._app = new App(serviceProvider, _configuration);

            SDKConfiguration.ServiceProvider = serviceProvider;

            SDKLoggerFactory.Init();
            
            return this._app;
        }

        public IAppBuilder BindEvents<T>() 
        {
            SDKConfiguration.BindEvents<T>();

            return this;
        }

        public IAppBuilder UseStartup<TStartup> () where TStartup : IStartup {

            TStartup instance = default(TStartup);

            var typeStartup = typeof (TStartup);
            var constructor = typeStartup.GetConstructor(new [] {typeof(IConfiguration)});
            if (constructor != null) {
                instance = (TStartup) constructor.Invoke(new []{this._configuration});
            } else {
                constructor = typeStartup.GetConstructor(new Type[0]);
                instance = (TStartup) constructor.Invoke(new object[0]);
            }
            
            _startup = instance;
            
            instance.ConfigureServices (this._services);
            
            return this;
        }

        public IAppBuilder BindDataMap<T>(string mapName) where T: Model
        {
            SDKDataMap.BindMap<T>(mapName);
            
            return this;
        }

        public IAppBuilder BindDataMap<T>() where T: Model
        {
            SDKDataMap.BindMap<T>();
            
            return this;
        }

        public IAppBuilder UseDataMap<T>() where T: IDataMapCollection
        {   
            SDKDataMap.BindsMapCollection<T>();
            
            return this;
        }

        public IApp RunSDK() 
        {
            var app = this.Build();

            if (_startup != null) _startup.Configure(this);
            
            app.Run();

            return app;
        }
    }
}