using System;
using System.Linq;
using ONS.SDK.Worker;
using ONS.SDK.Context;
using ONS.SDK.Data;
using ONS.SDK.Test.Web.Entities;
using Newtonsoft.Json;

namespace ONS.SDK.Test.Web.Process
{
    public class CenarioBusiness
    {
        [SDKEvent(CenarioEvent.InserirCenario)]
        public void InserirCenario(IContext<Conta> context) {
            Console.WriteLine("########## InserirCenario");
        }

        [SDKEvent(CenarioEvent.AlterarCenario)]
        public void AlterarCenario(IContext<Conta> context, 
            IDataSet<Conta> contas) {

            var conta = contas.FirstOrDefault();
            //var contas = context.DataContext.Set<Conta>();

            if (conta != null)
            {
                Console.WriteLine("########## Conta: " + conta.Id + ", Name: " + conta.Name);    
                conta.Name += "3";
                contas.Update(conta);
            }

            Console.WriteLine("########## " + JsonConvert.SerializeObject(contas));
        }
    }
}