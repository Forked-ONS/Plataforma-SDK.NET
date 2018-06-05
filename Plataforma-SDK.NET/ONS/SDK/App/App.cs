using Microsoft.Extensions.DependencyInjection;
using ONS.SDK.Context;
using ONS.SDK.App.Interfaces;
namespace ONS.SDK.App {
    public class App<TPayload> : IApp{

        private ContextBuilder<TPayload> _contextBuilder;
        public App(ContextBuilder<TPayload> builder) {
            this._contextBuilder = builder;
        }

        public void Run(){

        }

        public void AddServiceContainer(IServiceCollection services){

        }
    }
}