using System;
using System.Linq;
using ONS.SDK.Worker;
using ONS.SDK.Context;
using ONS.SDK.Data.Persistence;
using ONS.SDK.Test.Web.Entities;
using Newtonsoft.Json;

namespace ONS.SDK.Test.Web.Process
{
    public class CenarioBusiness
    {
        [SDKEvent(CenarioEvent.InserirCenario)]
        public void InserirCenario(Conta conta, IDataSet<Conta> contas) 
        {
            Console.WriteLine("########## InserirCenario");
                        
            if (conta != null)
            {
                Console.WriteLine("########## Conta: " + conta.Id + ", Name: " + conta.Name);    
                contas.Insert(conta);
            }
        }

        //[SDKEvent(CenarioEvent.AlterarCenario)]
        [SDKEvent(CenarioEvent.InserirCenario)]
        public void AlterarCenario(IContext<Conta> context, 
            IDataSet<Conta> contas) {

            var conta = context.Event.Payload;
            //var conta = contas.FirstOrDefault();
            //var contas = context.DataContext.Set<Conta>();

            if (conta != null)
            {
                Console.WriteLine("########## Conta: " + conta.Id + ", Name: " + conta.Name);    
                contas.Update(conta);
            }

            Console.WriteLine("########## " + JsonConvert.SerializeObject(contas));
        }
    }
}