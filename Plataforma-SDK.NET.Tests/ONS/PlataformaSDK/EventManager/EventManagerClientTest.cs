using Moq;
using NUnit.Framework;
using ONS.PlataformaSDK.EnvProps;
using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Entities;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.EventManager
{
    public class EventManagerClientTest
    {

        private EventManagerClient EventManagerClient;
        private Mock<HttpClient> HttpClientMock;
        private EnvironmentProperties EnvironmentProperties;
        
        [SetUp]
        public void Setup()
        {
            HttpClientMock = new Mock<HttpClient>();
            var ResultTask = Task.FromResult("{OK}");
            EnvironmentProperties = new EnvironmentProperties("http", "localhost", "8888");
            HttpClientMock.Setup(mock => mock.Put("http://localhost:8888/sendevent", It.IsAny<string>())).Returns(ResultTask);
            EventManagerClient = new EventManagerClient(HttpClientMock.Object, EnvironmentProperties);
        }

        [Test]
        public void Send()
        {
            var Event = new Event();
            Event.Name = "persist.request";
            Event.instanceId = "7777-7777";
            Event.Payload = new { instanceId = "999999" };
            EventManagerClient.SendEvent(Event);
            HttpClientMock.Verify(httpClient => httpClient.Put("http://localhost:8888/sendevent", 
                "{\"Name\":\"persist.request\",\"instanceId\":\"7777-7777\",\"Payload\":{\"instanceId\":\"999999\"}}"), Times.Once);
        }
    }
}