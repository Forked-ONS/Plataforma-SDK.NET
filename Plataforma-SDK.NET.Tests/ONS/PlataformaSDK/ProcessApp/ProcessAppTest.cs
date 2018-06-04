using Moq;
using Moq.Protected;
using NUnit.Framework;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Domain;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.EventManager;
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
        private ProcessAppImpl ProcessApp;
        private Mock<IExecutable> AppMock;
        private Mock<ProcessMemoryHttpClient> ProcessMemoryClientMock;
        private Mock<CoreClient> CoreClientMock;
        private Mock<DomainClient> DomainClientMock;
        private Mock<EventManagerClient> EventManagerClientMock;

        [SetUp]
        public void Setup()
        {
            ProcessMemoryClientMock = ProcessAppTestHelper.CreateProcessMemoryClientMock();
            CoreClientMock = ProcessAppTestHelper.CreateCoreClientMock();
            DomainClientMock = ProcessAppTestHelper.CreateDomainClientMock();
            EventManagerClientMock = ProcessAppTestHelper.CreateEventManagerMock();
            ProcessApp = new ProcessAppImpl(ProcessAppTestHelper.SYSTEM_ID, ProcessAppTestHelper.PROCESS_INSTANCE_ID, ProcessAppTestHelper.PROCESS_ID,
                ProcessAppTestHelper.EVENT_IN, new DomainTestContext(), ProcessMemoryClientMock.Object, CoreClientMock.Object,
                    DomainClientMock.Object, EventManagerClientMock.Object);
            AppMock = ProcessAppTestHelper.CreateAppMock();
            ProcessApp.App = AppMock.Object;
        }

        [Test]
        public void Start()
        {
            ProcessApp.Start();

            ProcessMemoryClientMock.Verify(processMemoryClient => processMemoryClient.Head(ProcessAppTestHelper.PROCESS_INSTANCE_ID), Times.Once);
            ProcessMemoryClientMock.Verify(processMemoryClientMock => processMemoryClientMock.Commit(ProcessApp.Context), Times.Exactly(2));
            CoreClientMock.Verify(coreClientMock => coreClientMock.OperationByProcessId(ProcessAppTestHelper.PROCESS_ID), Times.Once);
            CoreClientMock.Verify(coreClientMock => coreClientMock.MapByProcessId(ProcessAppTestHelper.PROCESS_ID), Times.Once);
            AppMock.Verify(appMock => appMock.Execute(It.IsAny<IDomainContext>(), It.IsAny<Context>()));
            EventManagerClientMock.Verify(eventManagerMock => eventManagerMock.SendEvent(It.IsAny<Event>()), Times.Once);

            Assert.AreEqual(ProcessAppTestHelper.EVENT_IN, ProcessApp.Context.Event.Name);
            Assert.AreEqual(ProcessAppTestHelper.PROCESS_ID, ProcessApp.Context.ProcessId);
            Assert.AreEqual(ProcessAppTestHelper.SYSTEM_ID, ProcessApp.Context.SystemId);
            Assert.AreEqual(ProcessAppTestHelper.PROCESS_INSTANCE_ID, ProcessApp.Context.InstanceId);
            Assert.AreEqual(ProcessAppTestHelper.EVENT_OUT, ProcessApp.Context.EventOut);
            Assert.AreEqual(ProcessAppTestHelper.EVENT_IN, ProcessApp.EventIn);
            Assert.True(ProcessApp.Context.Commit);
            Assert.False(ProcessApp.DataSetBuilt);
            Assert.NotNull(ProcessApp.Context.Map);
        }

        [Test]
        public void verifyOperationList()
        {
            ProcessApp.VerifyOperationList(ProcessAppTestHelper.GetOperationList());
        }

    }
}