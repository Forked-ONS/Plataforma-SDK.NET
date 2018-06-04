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
            ProcessMemoryClientMock.Verify(processMemoryClientMock => processMemoryClientMock.Commit(ProcessApp.Context), Times.Once);
            CoreClientMock.Verify(coreClientMock => coreClientMock.OperationByProcessId(ProcessAppTestHelper.PROCESS_ID), Times.Once);
            CoreClientMock.Verify(coreClientMock => coreClientMock.MapByProcessId(ProcessAppTestHelper.PROCESS_ID), Times.Once);
            AppMock.Verify(appMock => appMock.Execute(It.IsAny<IDomainContext>(), It.IsAny<Context>()));
            EventManagerClientMock.Verify(eventManagerMock => eventManagerMock.SendEvent(It.IsAny<Event>()), Times.Never());

            this.AssertCopy(ProcessApp.Context, ProcessAppTestHelper.GetReproductionProcessMemoryHead());
            Assert.True(ProcessApp.DataSetBuilt);
            Assert.AreEqual(EVENT_IN, ProcessApp.Context.Event.Name);
            Assert.AreEqual(ProcessAppTestHelper.PROCESS_ID, ProcessApp.Context.ProcessId);
            Assert.AreEqual(ProcessAppTestHelper.SYSTEM_ID, ProcessApp.Context.SystemId);
            Assert.AreEqual(ProcessAppTestHelper.PROCESS_INSTANCE_ID, ProcessApp.Context.InstanceId);
            Assert.AreEqual(ProcessAppTestHelper.EVENT_OUT, ProcessApp.Context.EventOut);
            Assert.AreEqual(EVENT_IN, ProcessApp.EventIn);
            Assert.True(ProcessApp.Context.Commit);
            Assert.NotNull(ProcessApp.Context.Map);
            Assert.NotNull(ProcessApp.Context.Event.Reproduction);
            Assert.AreEqual(ProcessAppTestHelper.ORIGINAL_PROCESS_INSTANCE_ID, ProcessApp.Context.Event.Reproduction.From);
            Assert.AreEqual(ProcessAppTestHelper.PROCESS_INSTANCE_ID, ProcessApp.Context.Event.Reproduction.To);
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
            //FIXME Equals
            // Assert.AreEqual(context.DataSet, processMemoryEntity.DataSet);
        }

    }
}