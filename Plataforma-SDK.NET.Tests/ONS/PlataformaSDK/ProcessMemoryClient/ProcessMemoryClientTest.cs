using Moq;
using NUnit.Framework;
using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Environment;
using System.Threading.Tasks;
using ONS.PlataformaSDK.ProcessMemoryClient;
using ONS.PlataformaSDK.Entities;

namespace ONS.PlataformaSDK.ProcessMemory
{
    public class ProcessMemoryClientTest
    {
        private const string PROCESS_INSTANCE_ID = "11de37cb-2a69-45d9-9a46-3b24f57eb25b";
        private const string URL_HEAD = "http://localhost:9091/11de37cb-2a69-45d9-9a46-3b24f57eb25b/head?app_origin=dotnet_sdk";
        private const string URL_COMMIT = "http://localhost:9091/11de37cb-2a69-45d9-9a46-3b24f57eb25b/commit?app_origin=dotnet_sdk";
        private const string CONTEXT_JSON = "{\"InstanceId\":\"11de37cb-2a69-45d9-9a46-3b24f57eb25b\",\"Commit\":false}";
        private const string HEAD_JSON = "{\"event\":{\"name\":\"presentation.insere.tarefa.request\",\"instance_id\":null,\"reference_date\":null,\"scope\":\"execution\",\"reproduction\":{},\"reprocess\":{},\"payload\":{\"nomeTarefa\":\"teste\"}}}";

        private ProcessMemoryHttpClient ProcessMemoryClient;
        private Mock<HttpClient> HttpClientMock;
        private EnvironmentProperties EnvironmentProperties;

        [SetUp]
        public void Setup()
        {
            HttpClientMock = new Mock<HttpClient>();
            var task = Task.FromResult(HEAD_JSON);
            HttpClientMock.Setup(mock => mock.Get(URL_HEAD)).Returns(task);
            HttpClientMock.Setup(mock => mock.Post(URL_COMMIT, CONTEXT_JSON));
            EnvironmentProperties = new EnvironmentProperties("http", "localhost", "9091");
            ProcessMemoryClient = new ProcessMemoryHttpClient(HttpClientMock.Object, EnvironmentProperties);
        }

        [Test]
        public void Head()
        {   
            var ProcessMemory = ProcessMemoryClient.Head(PROCESS_INSTANCE_ID).Result;
            HttpClientMock.Verify(httpClient => httpClient.Get(URL_HEAD), Times.Once);
            Assert.AreEqual("presentation.insere.tarefa.request", ProcessMemory.Event.Name);
            Assert.Null(ProcessMemory.Event.Instance_Id);
            Assert.Null(ProcessMemory.Event.Reference_Date);
            Assert.AreEqual("execution", ProcessMemory.Event.Scope);
            Assert.NotNull(ProcessMemory.Event.Reproduction);
            Assert.NotNull(ProcessMemory.Event.Reprocess);
            Assert.NotNull(ProcessMemory.Event.Payload);
        }

        [Test]
        public void Commit()
        {   
            var Context = new Context();
            Context.InstanceId = PROCESS_INSTANCE_ID;
            ProcessMemoryClient.Commit(Context);
            HttpClientMock.Verify(httpClient => httpClient.Post(URL_COMMIT, CONTEXT_JSON), Times.Once);
        }

    }
}