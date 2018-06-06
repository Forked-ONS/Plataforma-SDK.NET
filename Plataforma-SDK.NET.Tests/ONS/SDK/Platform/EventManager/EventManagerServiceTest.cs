using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ONS.SDK.Domain.Core;
using ONS.SDK.Infra;
using ONS.SDK.Platform.EventManager;
using ONS.SDK.Utils.Http;

namespace Plataforma_SDK.NET.Tests.ONS.SDK.Platform.EventManager {
    [TestFixture]
    public class EventManagerServiceTest {

        [Test]
        public void ShouldPushEventToEventManager () {
            Mock<HttpClient> mock = new Mock<HttpClient> ();
            mock.Setup (http => http.Put (It.IsAny<string> (), It.IsAny<string> ())).Returns (Task.FromResult (@"{ ""message"":""OK"" } "));
            var evt = new Event<Payload> ();
            evt.Payload = new Payload() {Id = "1"};

            var config = new EventManagerConfig ();
            var service = new EventManagerService(config, new JsonHttpClient (mock.Object));
            service.Push(evt);
            mock.Verify(h => h.Put(It.IsAny<string> (), @"{""payload"":{""id"":""1""}"));
        }
    }

    class Payload {
        public string Id { get; set; }
    }
}