using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ONS.SDK.Utils.Http;

namespace Plataforma_SDK.NET.Tests.ONS.SDK.Utils.Http {
    [TestFixture]
    public class JsonHttpClientTest {

        private readonly Mock<HttpClient> mock = new Mock<HttpClient> ();

        [Test]
        public void ShouldChangeJsonBetweenExternalService () {
            mock.Setup (s => s.Get ("get")).Returns (Task.FromResult(@"{ ""Nome"": ""Moneda"" } "));
            var client = new JsonHttpClient (mock.Object);
            var r = client.Get<Response> ("get");
            Assert.AreEqual (r.Nome, "Moneda");
        } 


        [Test]
        public void ShouldPostJsonObjectAndReceive(){
            var body = new Response {Nome="Moneda"}; 
            mock.Setup (s => s.Post (It.IsAny<string>(), It.IsAny<string>())).Returns (Task.FromResult(@"{ ""Nome"": ""Moneda2"" } "));
            var client = new JsonHttpClient (mock.Object);
            var r = client.Post<Response> ("post", body);
            Assert.AreEqual (r.Nome, "Moneda2");
        }

    }

    class Response {
        public string Nome { get; set; }
    }
}