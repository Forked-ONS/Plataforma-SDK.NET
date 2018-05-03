using Moq;
using NUnit.Framework;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.Exception;
using ONS.PlataformaSDK.ProcessApp;
using ONS.PlataformaSDK.ProcessMemoryClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.ProcessApp
{
    [TestFixture]
    public class ProcessAppTest
    {
        private const string PROCESS_INSTANCE_ID = "abaf4fbe-5359-41e7-a07c-8bd60191de56";
        private const string PROCESS_ID = "1448a166-a191-40e7-8c05-b1621f34ad73";
        private ProcessApp ProcessApp;
        private Mock<ProcessMemoryHttpClient> ProcessMemoryClientMock;
        private Mock<CoreClient> CoreClientMock;

        [SetUp]
        public void Setup()
        {
            CreateProcessMemoryClientMock();
            CreateCoreClientMock();
            ProcessApp = new ProcessApp(PROCESS_INSTANCE_ID, PROCESS_ID, ProcessMemoryClientMock.Object, CoreClientMock.Object);
        }

        [Test]
        public async Task Start()
        {
            await ProcessApp.Start();

            ProcessMemoryClientMock.Verify(processMemoryClient => processMemoryClient.Head(PROCESS_INSTANCE_ID), Times.Once);
            CoreClientMock.Verify(coreClientMock => coreClientMock.OperationByProcessIdAsync(PROCESS_ID), Times.Once);
            //FIXME Equals            
            Assert.AreEqual("presentation.insere.tarefa.request", ProcessApp.Context.Event.Name);
            Assert.AreEqual("1448a166-a191-40e7-8c05-b1621f34ad73", ProcessApp.Context.ProcessId);
            Assert.AreEqual("eb60a12f-130d-4b8b-8b0d-a5f94d39cb0", ProcessApp.Context.SystemId);
            Assert.AreEqual(PROCESS_INSTANCE_ID, ProcessApp.Context.InstanceId);
            Assert.AreEqual("presentation.insere.tarefa.request.done", ProcessApp.Context.EventOut);
            Assert.True(ProcessApp.Context.Commit);
        }

        [Test]
        public void verifyOperationList()
        {
            ProcessApp.VerifyOperationList(GetOperationList());
        }

        private void CreateProcessMemoryClientMock()
        {
            ProcessMemoryClientMock = new Mock<ProcessMemoryHttpClient>();
            ProcessMemoryClientMock.Setup(mock => mock.Head(PROCESS_INSTANCE_ID)).Returns(Task.FromResult(GetProcessMemoryHead()));
        }

        private void CreateCoreClientMock()
        {
            CoreClientMock = new Mock<CoreClient>();
            CoreClientMock.Setup(mock => mock.OperationByProcessIdAsync(PROCESS_ID)).Returns(Task.FromResult(GetOperationList()));
        }

        private void CreateCoreClientWithEmptyOperationsListMock()
        {
            CoreClientMock = new Mock<CoreClient>();
            CoreClientMock.Setup(mock => mock.OperationByProcessIdAsync(PROCESS_ID)).Returns(Task.FromResult(new List<Operation>()));
        }

        private ProcessMemoryEntity GetProcessMemoryHead()
        {
            var ProcessMemoryEntity = new ProcessMemoryEntity();
            ProcessMemoryEntity.Event = GetEvent();
            return ProcessMemoryEntity;
        }

        private List<Operation> GetOperationList()
        {
            var Operation = new Operation();
            Operation.ProcessId = "1448a166-a191-40e7-8c05-b1621f34ad73";
            Operation.SystemId = "eb60a12f-130d-4b8b-8b0d-a5f94d39cb0";
            Operation.Event_Out = "presentation.insere.tarefa.request.done" ;
            Operation.Commit = true;
            var OperationsList = new List<Operation>();
            OperationsList.Add(Operation);
            return OperationsList;
        }

        private Event GetEvent() {
            var Event = new Event();
            Event.Name = "presentation.insere.tarefa.request";
            return Event;
        }

    }
}