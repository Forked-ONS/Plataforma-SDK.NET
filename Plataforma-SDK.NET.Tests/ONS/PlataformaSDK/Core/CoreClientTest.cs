using Moq;
using NUnit.Framework;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Environment;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ONS.PlataformaSDK.Core
{
    public class CoreClientTest
    {
        private const string PROCESS_ID = "1448a166-a191-40e7-8c05-b1621f34ad73";
        private const string URL_FIND_OPERATION_BY_PROCESS_ID = "http://localhost:9110/core/operation?filter=byProcessId&processId=1448a166-a191-40e7-8c05-b1621f34ad73";
        private CoreClient CoreClient;
        private Mock<HttpClient> HttpClientMock;
        private EnvironmentProperties EnvironmentProperties;

        [SetUp]
        public void Setup()
        {
            HttpClientMock = new Mock<HttpClient>();
            var task = Task.FromResult(GetJsonOperationByProcessId());
            HttpClientMock.Setup(mock => mock.Get(URL_FIND_OPERATION_BY_PROCESS_ID)).Returns(task);
            EnvironmentProperties = new EnvironmentProperties("http", "localhost", "9110");
            CoreClient = new CoreClient(HttpClientMock.Object, EnvironmentProperties);
        }

        [Test]
        public void operationByProcessId()
        {
            List<Operation> Operations = CoreClient.OperationByProcessId(PROCESS_ID);
            HttpClientMock.Verify(httpClient => httpClient.Get(URL_FIND_OPERATION_BY_PROCESS_ID), Times.Once);
            // Assert.NotNull(Operations);
        }

        public string GetJsonOperationByProcessId()
        {
            return "[{\"_metadata\": {\"branch\": \"master\",\"type\": \"operation\"}," + 
                        "\"commit\": true," + 
                        "\"event_in\": \"presentation.insere.tarefa.request\"," + 
                        "\"event_out\": \"presentation.insere.tarefa.request.done\"," + 
                        "\"id\": \"d89036a1-de71-446e-a3f9-7b519c4247ac\"," + 
                        "\"image\": \"registry:5000/mantertarefasretificacaopresentation.app:fe6c0c2d-5859-48d9-9828-27578d7c975e\"," + 
                        "\"name\": \"presentation.insere.tarefa.request\"," + 
                        "\"processId\": \"1448a166-a191-40e7-8c05-b1621f34ad73\"," + 
                        "\"systemId\": \"eb60a12f-130d-4b8b-8b0d-a5f94d39cb0b\"" +
                    "}, {\"_metadata\": {\"branch\": \"master\", \"type\": \"operation\"}," + 
                        "\"commit\": true," + 
                        "\"event_in\": \"presentation.uploadplanilha.tarefa.request\"," + 
                        "\"event_out\": \"presentation.uploadplanilha.tarefa.request.done\"," + 
                        "\"id\": \"a2eedc5e-749a-4118-9780-98a09b308c4b\"," + 
                        "\"image\": \"registry:5000/mantertarefasretificacaopresentation.app:fe6c0c2d-5859-48d9-9828-27578d7c975e\"," + 
                        "\"name\": \"presentation.uploadplanilha.tarefa.request\"," + 
                        "\"processId\": \"1448a166-a191-40e7-8c05-b1621f34ad73\"," + 
                        "\"systemId\": \"eb60a12f-130d-4b8b-8b0d-a5f94d39cb0b\"}]";
         ;
        }

    }
}