using System;
using ONS.SDK.Builder;
using ONS.SDK.Builder.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ONS.SDK.Test.Console.Process.Worker;

namespace ONS.SDK.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AppBuilder.CreateDefaultBuilder(null)
            .UseStartup<Startup>()
            .BindEvents<CalculoTaxaProcess>()
            .RunSDK();
        }
    }

    class Startup : IStartup
    {
        public Startup(IConfiguration conf){}

        public void Configure(IAppBuilder appBuilder){
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<CalculoTaxaProcess>();
        }
    }
}
