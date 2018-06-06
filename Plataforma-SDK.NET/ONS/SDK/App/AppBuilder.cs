using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONS.SDK.App.Exceptions;
using ONS.SDK.App.Interfaces;
using ONS.SDK.Context;
using ONS.SDK.Domain.Interfaces;
using ONS.SDK.Infra;
using ONS.SDK.Platform.Core;
using ONS.SDK.Utils.Http;
using Plataforma_SDK.NET.ONS.SDK.Domain.Core;
using Plataforma_SDK.NET.ONS.SDK.Domain.Services;

namespace ONS.SDK.App {
    public class AppBuilder : IAppBuilder {

        //Cria um builder com configurações default
        private IApp _app;

        private IConfiguration _configuration;

        private IServiceCollection _services;

        private ApplicationContext _appContext;
        public AppBuilder () {
            this._services = new ServiceCollection ();
            var builder = new ConfigurationBuilder ();
            this._configuration = builder.AddEnvironmentVariables ().Build ();
            this._services.AddSingleton(typeof(ProcessMemoryConfig), new ProcessMemoryConfig(this._configuration));
            this._services.AddSingleton(typeof(EventManagerConfig), new EventManagerConfig(this._configuration));
            this._services.AddSingleton(typeof(ExecutorConfig), new ExecutorConfig(this._configuration));
            this._services.AddSingleton(typeof(CoreConfig), new CoreConfig(this._configuration));
            this._services.AddSingleton(typeof(JsonHttpClient), new JsonHttpClient());
            this._services.AddSingleton(typeof(ApplicationContext), new ApplicationContext(this._configuration));
            this._services.AddSingleton<IInstalledAppService, InstalledAppService>();
        }

        public static IAppBuilder CreateDefaultBuilder (string[] args) {
            var builder = new AppBuilder ();
            return builder;
        }

        public IApp Build () {
            if (this._app == null) {
                throw new BadConfigException ("Missing configuration to build app, you should config UseEventPayloadType");
            }
            this._app.AddServiceContainer (this._services);
            return this._app;
        }

        public IAppBuilder UseDomain<T> () where T : class, IDomainContext {
            this._services.AddSingleton<IDomainContext, T> ();
            return this;
        }

        public IAppBuilder UseEventPayloadType<TPayload> () {
            var builder = new ContextBuilder<TPayload> ();
            var app = new App<TPayload> (builder);
            this._app = app;
            return this;
        }

        public IAppBuilder UseRunner<TRunner> () where TRunner : class, IRunnable {
            this._services.AddSingleton<IRunnable, TRunner> ();
            return this;
        }

        public IAppBuilder UseStartup<TStartup> () where TStartup : class {
            var instance = Activator.CreateInstance (typeof (TStartup), this._configuration) as IStartup;
            instance.ConfigureServices (this._services);
            return this;
        }
    }
}