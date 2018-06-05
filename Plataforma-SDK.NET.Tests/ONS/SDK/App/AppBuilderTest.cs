using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ONS.SDK.App;
using ONS.SDK.App.Interfaces;
using ONS.SDK.Domain.Interfaces;

namespace ONS.SDK.AppTest {

    [TestFixture]
    class AppBuilderTest {

        [Test]
        public void ShouldBuildApp(){
            var app = AppBuilder.CreateDefaultBuilder(null)
            .UseStartup<Startup>()
            .UseDomain<Domain>()
            .UseEventPayloadType<Payload>()
            .UseRunner<Runner>()
            .Build();

            Assert.AreEqual(true, Startup.PassHere);
            Assert.IsNotNull(app.Provider.GetService(typeof(IDomainContext)));
            Assert.IsNotNull(app.Provider.GetService(typeof(IRunnable)));
        }
    }

    class Startup : IStartup
    {
        public static bool PassHere {get;set;}

        public Startup(IConfiguration conf){}

        public void ConfigureServices(IServiceCollection services) => Startup.PassHere = true;
    }

    class Domain : IDomainContext
    {

    }

    class Payload {

    }

    class Runner : IRunnable
    {
        public void Run()
        {

        }
    }
}