using Moq;
using Moq.Protected;
using NUnit.Framework;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Entities;
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
            ProcessMemoryClientMock.Setup(mock => mock.Head(PROCESS_INSTANCE_ID)).Returns(Task.FromResult(GetProcessMemoryHead()));
            return ProcessMemoryClientMock;
        }

        public static Mock<CoreClient> CreateCoreClientMock()
        {
            var CoreClientMock = new Mock<CoreClient>();
            CoreClientMock.Setup(mock => mock.OperationByProcessIdAsync(PROCESS_ID)).Returns(Task.FromResult(GetOperationList()));
            CoreClientMock.Setup(mock => mock.MapByProcessId(PROCESS_ID)).Returns(Task.FromResult(GetMapList()));
            return CoreClientMock;
        }

        public static Mock<CoreClient> CreateCoreClientWithEmptyOperationsListMock()
        {
            var CoreClientMock = new Mock<CoreClient>();
            CoreClientMock.Setup(mock => mock.OperationByProcessIdAsync(PROCESS_ID)).Returns(Task.FromResult(new List<Operation>()));
            return CoreClientMock;
        }

        public static ProcessMemoryEntity GetProcessMemoryHead()
        {
            var ProcessMemoryEntity = new ProcessMemoryEntity();
            ProcessMemoryEntity.Event = GetEvent();
            return ProcessMemoryEntity;
        }

        public static List<Operation> GetOperationList()
        {
            var Operation = new Operation();
            var OperationsList = new List<Operation>();
            OperationsList.Add(createOperation("presentation.exclui.tarefa.request"));
            OperationsList.Add(createOperation("presentation.insere.tarefa.request"));
            return OperationsList;
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
            return new List<PlatformMap>();
        }
    }
}