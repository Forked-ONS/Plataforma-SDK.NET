using Moq;
using NUnit.Framework;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Environment;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var OperationsTask = CoreClient.OperationByProcessIdAsync(PROCESS_ID);
            HttpClientMock.Verify(httpClient => httpClient.Get(URL_FIND_OPERATION_BY_PROCESS_ID), Times.Once);
            var Operations = OperationsTask.Result;

            Assert.IsTrue(Operations[0].Commit);
            Assert.AreEqual("presentation.insere.tarefa.request", Operations[0].Event_In);
            Assert.AreEqual("presentation.insere.tarefa.request.done", Operations[0].Event_Out);
            Assert.AreEqual("d89036a1-de71-446e-a3f9-7b519c4247ac", Operations[0].Id);
            Assert.AreEqual("registry:5000/mantertarefasretificacaopresentation.app:fe6c0c2d-5859-48d9-9828-27578d7c975e", Operations[0].Image);
            Assert.AreEqual("presentation.insere.tarefa.request", Operations[0].Name);
            Assert.AreEqual("1448a166-a191-40e7-8c05-b1621f34ad73", Operations[0].ProcessId);
            Assert.AreEqual("eb60a12f-130d-4b8b-8b0d-a5f94d39cb0b", Operations[0].SystemId);

            Assert.IsTrue(Operations[1].Commit);
            Assert.AreEqual("presentation.uploadplanilha.tarefa.request", Operations[1].Event_In);
            Assert.AreEqual("presentation.uploadplanilha.tarefa.request.done", Operations[1].Event_Out);
            Assert.AreEqual("a2eedc5e-749a-4118-9780-98a09b308c4b", Operations[1].Id);
            Assert.AreEqual("registry:5000/mantertarefasretificacaopresentation.app:fe6c0c2d-5859-48d9-9828-27578d7c975e", Operations[1].Image);
            Assert.AreEqual("presentation.uploadplanilha.tarefa.request", Operations[1].Name);
            Assert.AreEqual("1448a166-a191-40e7-8c05-b1621f34ad73", Operations[1].ProcessId);
            Assert.AreEqual("eb60a12f-130d-4b8b-8b0d-a5f94d39cb0b", Operations[1].SystemId);
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