using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ONS.SDK.Builder;
using ONS.SDK.Extensions.Builder;
using ONS.SDK.Impl.Builder;

namespace ONS.SDK.AppTest {

    [TestFixture]
    class AppBuilderTest {

        [Test]
        public void ShouldBuildApp(){
            var app = AppBuilder.CreateDefaultBuilder(null)
            .UseStartup<Startup>()
            .RunSDK(); 

            //Assert.AreEqual(true, Startup.PassHere);
        }
    }

    class Startup : ONS.SDK.Builder.IStartup
    {
        public static bool PassHere {get;set;}

        public Startup(IConfiguration conf){}

        public void ConfigureServices(IServiceCollection services) => Startup.PassHere = true;

        public void Configure(IAppBuilder app)
        {
            
        }
    }

    class Payload {

    }

}