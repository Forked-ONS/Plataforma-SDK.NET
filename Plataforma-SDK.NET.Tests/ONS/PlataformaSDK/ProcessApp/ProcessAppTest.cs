using Moq;
using NUnit.Framework;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.ProcessApp;
using ONS.PlataformaSDK.ProcessMemory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class ProcessAppTest
    {
        private const string PROCESS_INSTANCE_ID = "abaf4fbe-5359-41e7-a07c-8bd60191de56";
        private ProcessApp ProcessApp;
        private Mock<ProcessMemoryClient> ProcessMemoryClientMock;
        private Mock<CoreClient> CoreClientMock;

        [SetUp]
        public void Setup()
        {
            CreateProcessMemoryClientMock();
            CoreClientMock = new Mock<CoreClient>();
            ProcessApp = new ProcessApp(PROCESS_INSTANCE_ID, ProcessMemoryClientMock.Object, CoreClientMock.Object);
        }

        [Test]
        public void Start()
        {   
            ProcessApp.Start();
            ProcessMemoryClientMock.Verify(processMemoryClient => processMemoryClient.Head(PROCESS_INSTANCE_ID), Times.Once);
            Assert.AreEqual("presentation.exclui.tarefa.request", ProcessApp.Context.Event.Name);
            Assert.AreEqual("execution", ProcessApp.Context.Event.Scope);
            Assert.NotNull(ProcessApp.Context.Event.Payload);
            Assert.AreEqual("Teste", (string) ProcessApp.Context.Event.Payload["nomeTarefa"]);
        }

        private void CreateProcessMemoryClientMock(){
            ProcessMemoryClientMock = new Mock<ProcessMemoryClient>();
            ProcessMemoryClientMock.Setup(mock => mock.Head(PROCESS_INSTANCE_ID)).Returns(Task.FromResult(GetProcessMemoryHead()));
        }

        private string GetProcessMemoryHead() {
            return "{\"name\":\"presentation.exclui.tarefa.request\",\"instance_id\":null,\"reference_date\":null,\"scope\":\"execution\",\"reproduction\":{},\"reprocess\":{},\"payload\":{\"tarefa\":{\"_metadata\":{\"branch\":\"master\",\"instance_id\":\"2c086980-de88-4990-8e33-683b6d871ee4\",\"type\":\"tarefaretificacao\"},\"id\":\"85fd51bb-3739-409f-bcb0-40fe6a52593f\",\"nome\":\"Teste\",\"situacao\":null},\"nomeTarefa\":\"Teste\"}}";
        }

    }
}