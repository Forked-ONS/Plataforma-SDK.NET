using ONS.SDK.Worker;
using ONS.SDK.Context;

namespace ONS.SDK.Test.Console.Process.Worker
{
    public class CalculoTaxaProcess
    {
        [SDKEvent]
        public void CalcularTaxasPorUsina(IContext<CalculoTaxasPorUsinaPayload> context) {
            
            System.Console.WriteLine("####################### CalcularTaxasPorUsina");
        }
        
    }
}