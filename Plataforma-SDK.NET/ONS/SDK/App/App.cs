using System;
using Microsoft.Extensions.DependencyInjection;
using ONS.SDK.App.Interfaces;
using ONS.SDK.Context;

namespace ONS.SDK.App {
    public class App<TPayload> : IApp {

        private ContextBuilder<TPayload> _contextBuilder;
        private IServiceCollection _services;

        public IServiceProvider Provider => this._services.BuildServiceProvider ();

        public App (ContextBuilder<TPayload> builder) {
            this._contextBuilder = builder;
        }

        public void Run () {
            var context = this._contextBuilder.Build ("");
        }

        public void AddServiceContainer (IServiceCollection services) {
            this._services = services;
        }
    }
}