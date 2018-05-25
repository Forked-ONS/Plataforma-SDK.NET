using System;
using ONS.PlataformaSDK.ProcessApp;
using ONS.PlataformaSDK.Main;
using Executable;
using DomainContext;

namespace cadastro_cliente.Process
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cadastro de conta");
            System.Environment.SetEnvironmentVariable("SYSTEM_ID", "ec498841-59e5-47fd-8075-136d79155705");
            System.Environment.SetEnvironmentVariable("INSTANCE_ID", "2a2b862a-0816-4d33-8f23-75f75d9fd601");
            System.Environment.SetEnvironmentVariable("PROCESS_ID", "f7fe8464-030f-4e85-9ab9-d91adb1018ff");
            System.Environment.SetEnvironmentVariable("EVENT", "create.client.request");
            try {
                var MainApp = new MainApp();
                MainApp.Execute(new CadastraCliente(), new DomainContextConta());
            } catch(Exception e) {
                e.ToString();
            }

        }
    }
}
