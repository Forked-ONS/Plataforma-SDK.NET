using Moq;
using NUnit.Framework;
using ONS.PlataformaSDK.ProcessApp;
using ONS.PlataformaSDK.ProcessMemory;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class ProcessAppTest
    {
        private ProcessApp ProcessApp;

        [SetUp]
        public void Setup()
        {
            var ProcessMemoryClientMock = new Mock<ProcessMemoryClient>();
            ProcessApp = new ProcessApp("", ProcessMemoryClientMock.Object);
        }

        [Test]
        public void ParseEvent()
        {   
            var Event = ProcessApp.ParseEvent(GetHead());
            Assert.AreEqual("presentation.exclui.tarefa.request", Event.Name());
        }

        private string GetHead() {
            return "{\"name\":\"presentation.exclui.tarefa.request\",\"instance_id\":null,\"reference_date\":null,\"scope\":\"execution\",\"reproduction\":{},\"reprocess\":{},\"payload\":{\"tarefa\":{\"_metadata\":{\"branch\":\"master\",\"instance_id\":\"2c086980-de88-4990-8e33-683b6d871ee4\",\"type\":\"tarefaretificacao\"},\"id\":\"85fd51bb-3739-409f-bcb0-40fe6a52593f\",\"nome\":\"Teste\",\"situacao\":null},\"nomeTarefa\":\"Teste\"}}";
        }

    }
}