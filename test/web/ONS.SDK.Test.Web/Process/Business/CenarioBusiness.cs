using System;
using ONS.SDK.Worker;
using ONS.SDK.Context;

namespace ONS.SDK.Test.Web.Process
{
    public class CenarioBusiness
    {
        [SDKEvent(CenarioEvent.InserirCenario)]
        public void InserirCenario(IContext<InseriCenarioPayload> context) {
            Console.WriteLine("########## InserirCenario");
        }

        [SDKEvent(CenarioEvent.AlterarCenario)]
        public void AlterarCenario(IContext<AlteraCenarioPayload> context) {
            Console.WriteLine("########## " + context.Event.Payload);
        }
    }
}