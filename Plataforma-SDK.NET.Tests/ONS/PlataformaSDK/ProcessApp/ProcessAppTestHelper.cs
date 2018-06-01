using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Domain;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.EventManager;
using ONS.PlataformaSDK.Exception;
using ONS.PlataformaSDK.ProcessApp;
using ONS.PlataformaSDK.ProcessMemoryClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class ProcessAppTestHelper
    {
        public const string PROCESS_INSTANCE_ID = "abaf4fbe-5359-41e7-a07c-8bd60191de56";
        public const string PROCESS_ID = "1448a166-a191-40e7-8c05-b1621f34ad73";

        public static Mock<ProcessMemoryHttpClient> CreateProcessMemoryClientMock()
        {
            var ProcessMemoryClientMock = new Mock<ProcessMemoryHttpClient>();
            ProcessMemoryClientMock.Setup(mock => mock.Head(PROCESS_INSTANCE_ID)).Returns(GetProcessMemoryHead());
            return ProcessMemoryClientMock;
        }

        public static Mock<ProcessMemoryHttpClient> CreateReproductionProcessMemoryClientMock()
        {
            var ProcessMemoryClientMock = new Mock<ProcessMemoryHttpClient>();
            ProcessMemoryClientMock.Setup(mock => mock.Head(PROCESS_INSTANCE_ID)).Returns(GetReproductionProcessMemoryHead());
            return ProcessMemoryClientMock;
        }

        public static Mock<CoreClient> CreateCoreClientMock()
        {
            var CoreClientMock = new Mock<CoreClient>();
            CoreClientMock.Setup(mock => mock.OperationByProcessId(PROCESS_ID)).Returns(GetOperationList());
            CoreClientMock.Setup(mock => mock.MapByProcessId(PROCESS_ID)).Returns(GetMapList());
            return CoreClientMock;
        }

        internal static Mock<EventManagerClient> CreateEventManagerMock()
        {
            var EventManagerMock = new Mock<EventManagerClient>();
            EventManagerMock.Setup(mock => mock.SendEvent(CreateEvent()));
            return EventManagerMock;
        }

        internal static Mock<DomainClient> CreateDomainClientMock()
        {
            return new Mock<DomainClient>();
        }

        internal static Mock<IExecutable> CreateAppMock()
        {
            Mock<IExecutable> ExecutableMock = new Mock<IExecutable>();
            return ExecutableMock;
        }

        internal static Mock<CoreClient> CreateCoreClientWithEmptyOperationsListMock()
        {
            var CoreClientMock = new Mock<CoreClient>();
            CoreClientMock.Setup(mock => mock.OperationByProcessId(PROCESS_ID)).Returns(new List<Operation>());
            return CoreClientMock;
        }

        internal static ProcessMemoryEntity GetProcessMemoryHead()
        {
            var ProcessMemoryEntity = new ProcessMemoryEntity();
            ProcessMemoryEntity.Event = GetEvent();
            return ProcessMemoryEntity;
        }

        internal static ProcessMemoryEntity GetReproductionProcessMemoryHead()
        {
            var ProcessMemoryEntity = new ProcessMemoryEntity();
            ProcessMemoryEntity.Event = GetEvent();
            ProcessMemoryEntity.DataSet = new DataSet();
            ProcessMemoryEntity.DataSet.Entities = new DomainTestContext();
            return ProcessMemoryEntity;
        }

        internal static List<Operation> GetOperationList()
        {
            var Operation = new Operation();
            var OperationsList = new List<Operation>();
            OperationsList.Add(createOperation("presentation.exclui.tarefa.request"));
            OperationsList.Add(createOperation("presentation.insere.tarefa.request"));
            return OperationsList;
        }

        internal static Event CreateEvent()
        {
            var Event = new Event();
            Event.instanceId = "eb60a12f-130d-4b8b-8b0d-a5f94d39cb0";
            Event.Name = "eb60a12f-130d-4b8b-8b0d-a5f94d39cb0persist.request";
            Event.Payload = JObject.Parse("{instanceId:\"abaf4fbe-5359-41e7-a07c-8bd60191de56\"}");
            return Event;
        }

        public static Operation createOperation(string eventInName)
        {
            var Operation = new Operation();
            Operation.ProcessId = "1448a166-a191-40e7-8c05-b1621f34ad73";
            Operation.SystemId = "eb60a12f-130d-4b8b-8b0d-a5f94d39cb0";
            Operation.Event_In = eventInName;
            Operation.Event_Out = eventInName + ".done";
            Operation.Commit = true;
            return Operation;
        }

        public static Event GetEvent()
        {
            var Event = new Event();
            Event.Name = "presentation.insere.tarefa.request";
            return Event;
        }

        private static List<PlatformMap> GetMapList()
        {
            var PlatformMap = new PlatformMap();
            var Maps = new List<PlatformMap>();
            Maps.Add(PlatformMap);
            return Maps;
        }
    }
}