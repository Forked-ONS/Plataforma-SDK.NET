using Microsoft.Extensions.DependencyInjection;

namespace ONS.SDK.App.Interfaces {
    public interface IStartup {

        void ConfigureServices (IServiceCollection services);
    }
}