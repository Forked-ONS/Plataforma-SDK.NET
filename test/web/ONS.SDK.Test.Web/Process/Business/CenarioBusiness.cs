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

        [SDKEvent(CenarioEvent.AlterarCenario)]
        public void AlterarCenario(IContext<Conta> context, 
            IDataSet<Conta> contas) {

            var conta = context.Event.Payload;
            
            if (conta != null)
            {
                Console.WriteLine("########## Conta: " + conta.Id + ", Name: " + conta.Name);    
                contas.Update(conta);
            }

            Console.WriteLine("########## " + JsonConvert.SerializeObject(contas));
        }

        [SDKEvent(CenarioEvent.ExcluirCenario)]
        public void ExcluirCenario(Conta conta, IDataSet<Conta> contas) {

            if (conta != null)
            {
                Console.WriteLine("########## Conta: " + conta.Id);    
                contas.DeleteById(conta.Id);
            }

            Console.WriteLine("########## " + JsonConvert.SerializeObject(contas));
        }
    }
}