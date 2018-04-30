using Moq;
using NUnit.Framework;
using ONS.PlataformaSDK.ProcessMemory;
using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Environment;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.ProcessMemory
{
    public class ProcessMemoryClientTest
    {
        private const string PROCESS_INSTANCE_ID = "11de37cb-2a69-45d9-9a46-3b24f57eb25b";
        private const string URL_HEAD = "http://localhost:9091/11de37cb-2a69-45d9-9a46-3b24f57eb25b/head";
        private ProcessMemoryClient ProcessMemoryClient;
        private Mock<HttpClient> HttpClientMock;
        private EnvironmentProperties EnvironmentProperties;

        [SetUp]
        public void Setup()
        {
            HttpClientMock = new Mock<HttpClient>();
            var task = Task.FromResult(GetHead());
            HttpClientMock.Setup(mock => mock.Get(URL_HEAD)).Returns(task);
            EnvironmentProperties = new EnvironmentProperties("http", "localhost", "9091");
            ProcessMemoryClient = new ProcessMemoryClient(HttpClientMock.Object, EnvironmentProperties);
        }

        [Test]
        public void Head()
        {   
            Assert.AreEqual(GetHead(), ProcessMemoryClient.Head(PROCESS_INSTANCE_ID).Result);
            HttpClientMock.Verify(httpClient => httpClient.Get(URL_HEAD), Times.Once);
        }

        private string GetHead() {
            return "{\"name\":\"presentation.exclui.tarefa.request\",\"instance_id\":null,\"reference_date\":null,\"scope\":\"execution\",\"reproduction\":{},\"reprocess\":{},\"payload\":{\"tarefa\":{\"_metadata\":{\"branch\":\"master\",\"instance_id\":\"2c086980-de88-4990-8e33-683b6d871ee4\",\"type\":\"tarefaretificacao\"},\"id\":\"85fd51bb-3739-409f-bcb0-40fe6a52593f\",\"nome\":\"Teste\",\"situacao\":null},\"nomeTarefa\":\"Teste\"}}";
        }

    }
}