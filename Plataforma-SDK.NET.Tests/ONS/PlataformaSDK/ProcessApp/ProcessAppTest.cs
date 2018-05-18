using Moq;
using Moq.Protected;
using NUnit.Framework;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Domain;
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
        private const string EVENT_IN = "presentation.insere.tarefa.request";
        private const string SYSTEM_ID = "eb60a12f-130d-4b8b-8b0d-a5f94d39cb0";
        private ProcessApp ProcessApp;
        private Mock<ProcessMemoryHttpClient> ProcessMemoryClientMock;
        private Mock<CoreClient> CoreClientMock;
        private Mock<DomainClient> DomainClientMock;

        [SetUp]
        public void Setup()
        {
            ProcessMemoryClientMock = ProcessAppTestHelper.CreateProcessMemoryClientMock();
            CoreClientMock = ProcessAppTestHelper.CreateCoreClientMock();
            DomainClientMock = ProcessAppTestHelper.CreateDomainClientMock();
            ProcessApp = new ProcessApp(SYSTEM_ID, ProcessAppTestHelper.PROCESS_INSTANCE_ID, ProcessAppTestHelper.PROCESS_ID,
                EVENT_IN, new DomainTestContext(), ProcessMemoryClientMock.Object, CoreClientMock.Object, DomainClientMock.Object);
        }

        [Test]
        public async Task Start()
        {
            await ProcessApp.Start();

            ProcessMemoryClientMock.Verify(processMemoryClient => processMemoryClient.Head(ProcessAppTestHelper.PROCESS_INSTANCE_ID), Times.Once);
            CoreClientMock.Verify(coreClientMock => coreClientMock.OperationByProcessIdAsync(ProcessAppTestHelper.PROCESS_ID), Times.Once);
            CoreClientMock.Verify(coreClientMock => coreClientMock.MapByProcessId(ProcessAppTestHelper.PROCESS_ID), Times.Once);

            //FIXME Equals            
            Assert.AreEqual(EVENT_IN, ProcessApp.Context.Event.Name);
            Assert.AreEqual("1448a166-a191-40e7-8c05-b1621f34ad73", ProcessApp.Context.ProcessId);
            Assert.AreEqual(SYSTEM_ID, ProcessApp.Context.SystemId);
            Assert.AreEqual(ProcessAppTestHelper.PROCESS_INSTANCE_ID, ProcessApp.Context.InstanceId);
            Assert.AreEqual("presentation.insere.tarefa.request.done", ProcessApp.Context.EventOut);
            Assert.AreEqual(EVENT_IN, ProcessApp.EventIn);
            Assert.True(ProcessApp.Context.Commit);

            Assert.NotNull(ProcessApp.Context.Map);
        }

        [Test]
        public async Task StartProcess()
        {
            await ProcessApp.StartProcess();
            CoreClientMock.Verify(coreClientMock => coreClientMock.MapByProcessId(ProcessAppTestHelper.PROCESS_ID), Times.Once);
        }

        [Test]
        public void verifyOperationList()
        {
            ProcessApp.VerifyOperationList(ProcessAppTestHelper.GetOperationList());
        }

    }
}