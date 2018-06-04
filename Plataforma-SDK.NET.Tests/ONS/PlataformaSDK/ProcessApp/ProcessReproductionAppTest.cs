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
    public class ProcessReproductionAppTest
    {
        private const string EVENT_IN = "presentation.insere.tarefa.request";
        private const string SYSTEM_ID = "eb60a12f-130d-4b8b-8b0d-a5f94d39cb0";
        private ProcessAppImpl ProcessApp;
        private Mock<IExecutable> AppMock;
        private Mock<ProcessMemoryHttpClient> ProcessMemoryClientMock;
        private Mock<CoreClient> CoreClientMock;
        private Mock<DomainClient> DomainClientMock;
        private Mock<EventManagerClient> EventManagerClientMock;

        [SetUp]
        public void Setup()
        {
            ProcessMemoryClientMock = ProcessAppTestHelper.CreateReproductionProcessMemoryClientMock();
            CoreClientMock = ProcessAppTestHelper.CreateCoreClientMock();
            DomainClientMock = ProcessAppTestHelper.CreateDomainClientMock();
            EventManagerClientMock = ProcessAppTestHelper.CreateEventManagerMock();
            ProcessApp = new ProcessAppImpl(SYSTEM_ID, ProcessAppTestHelper.PROCESS_INSTANCE_ID, ProcessAppTestHelper.PROCESS_ID,
                EVENT_IN, new DomainTestContext(), ProcessMemoryClientMock.Object, CoreClientMock.Object,
                    DomainClientMock.Object, EventManagerClientMock.Object);
            AppMock = ProcessAppTestHelper.CreateAppMock();
            ProcessApp.App = AppMock.Object;
        }

        [Test]
        public void StartWithReproduction()
        {
            ProcessApp.Start();

            ProcessMemoryClientMock.Verify(processMemoryClient => processMemoryClient.Head(ProcessAppTestHelper.PROCESS_INSTANCE_ID), Times.Once);
            Assert.True(ProcessApp.DataSetBuilt);
            this.AssertCopy(ProcessApp.Context, ProcessAppTestHelper.GetReproductionProcessMemoryHead());
        }

        private void AssertCopy(Context context, ProcessMemoryEntity processMemoryEntity)
        {
            Assert.AreEqual(context.Event, processMemoryEntity.Event);
            Assert.AreEqual(context.ProcessId, processMemoryEntity.ProcessId);
            Assert.AreEqual(context.SystemId, processMemoryEntity.SystemId);
            Assert.AreEqual(context.InstanceId, processMemoryEntity.InstanceId);
            Assert.AreEqual(context.EventOut, processMemoryEntity.EventOut);
            Assert.AreEqual(context.Commit, processMemoryEntity.Commit);
            Assert.AreEqual(context.Map, processMemoryEntity.Map);
            //FIXME 
            //Assert.AreEqual(context.DataSet, processMemoryEntity.DataSet);
        }

    }
}