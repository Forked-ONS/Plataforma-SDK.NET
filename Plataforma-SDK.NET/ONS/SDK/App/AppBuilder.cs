using System;
using ONS.SDK.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ONS.SDK.App.Interfaces;
using ONS.SDK.Domain.Interfaces;

namespace ONS.SDK.App
{
    public class AppBuilder : IAppBuilder {

        //Cria um builder com configurações default
        private IApp _app;

        private IConfiguration _configuration;

        private IServiceCollection _services;

        public AppBuilder() {
            this._services = new ServiceCollection();
            var builder = new ConfigurationBuilder();
            this._configuration = builder.AddEnvironmentVariables().Build();

        }

        public static IAppBuilder CreateDefaultBuilder(string[] args) {
            var builder = new AppBuilder();
            return builder;
        }

        public IApp Build()
        {
            this._app.AddServiceContainer(this._services);
            return this._app;
        }

        public IAppBuilder UseDomain<T>() where T:class,IDomainContext
        {
            this._services.AddSingleton<IDomainContext,T>();
            return this;
        }

        public IAppBuilder UseEventPayloadType<TPayload>()
        {
            var builder = new ContextBuilder<TPayload>();
            var app = new App<TPayload>(builder);
            this._app = app;
            return this;
        }

        public IAppBuilder UseRunner<TRunner>() where TRunner:class,IRunnable
        {
            this._services.AddSingleton<IRunnable, TRunner>();
            return this;
        }

        public IAppBuilder UseStartup<TStartup>() where TStartup:class
        {
            var instance = Activator.CreateInstance(typeof(TStartup), this._configuration) as IStartup;
            instance.ConfigureServices(this._services);
            return this;
        }
    }
}