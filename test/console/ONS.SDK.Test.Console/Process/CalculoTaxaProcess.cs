using ONS.SDK.Context;
using ONS.SDK.Worker;

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