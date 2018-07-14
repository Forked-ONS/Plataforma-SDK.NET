using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ONS.SDK.Builder {
    public interface IStartup 
    {
        void Configure(IAppBuilder app);

        void ConfigureServices(IServiceCollection services);
        
    }
}