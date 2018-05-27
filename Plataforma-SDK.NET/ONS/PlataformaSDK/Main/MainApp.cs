using System;
using ONS.PlataformaSDK.Domain;
using ONS.PlataformaSDK.ProcessApp;
using ONS.PlataformaSDK.ProcessMemoryClient;
using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.EnvProps;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.EventManager;
using ONS.PlataformaSDK.Exception;

namespace ONS.PlataformaSDK.Main
{
    public class MainApp
    {
        public void Execute(IExecutable executable, IDomainContext domainContext)
        {
            var SystemId = GetEnvironmentVariable("SYSTEM_ID", null);
            var ProcessInstanceId = GetEnvironmentVariable("INSTANCE_ID", null);
            var ProcessId = GetEnvironmentVariable("PROCESS_ID", null);
            var EventIn = this.GetEnvironmentVariable("EVENT", null);
            var HttpClient = new HttpClient();

            var CoreClient = GetCoreClient(HttpClient);
            var ProcessApp = new ProcessAppImpl(SystemId, ProcessInstanceId, ProcessId, EventIn, domainContext, GetProcessMemoryClient(HttpClient), 
                GetCoreClient(HttpClient), GetDomainClient(HttpClient, CoreClient, SystemId), GetEventManagerClient(HttpClient));
            ProcessApp.App = executable;
            ProcessApp.Start();
        }

        private ProcessMemoryHttpClient GetProcessMemoryClient(HttpClient httpClient)
        {
            String Host = GetEnvironmentVariable("PROCESS_MEMORY_HOST", "localhost");
            String Scheme = GetEnvironmentVariable("PROCESS_MEMORY_SCHEME", "http");
            String Port = GetEnvironmentVariable("PROCESS_MEMORY_PORT", "9091");
            return new ProcessMemoryHttpClient(httpClient, new EnvironmentProperties(Scheme, Host, Port));
        }

        private CoreClient GetCoreClient(HttpClient httpClient)
        {
            String Host = GetEnvironmentVariable("COREAPI_HOST", "localhost");
            String Scheme = GetEnvironmentVariable("COREAPI_SCHEME", "http");
            String Port = GetEnvironmentVariable("COREAPI_PORT", "9110");
            return new CoreClient(httpClient, new EnvironmentProperties(Scheme, Host, Port));
        }

        private EventManagerClient GetEventManagerClient(HttpClient httpClient)
        {
            String Host = GetEnvironmentVariable("EVENT_MANAGER_HOST", "localhost");
            String Scheme = GetEnvironmentVariable("EVENT_MANAGER_SCHEME", "http");
            String Port = GetEnvironmentVariable("EVENT_MANAGER_PORT", "8081");
            return new EventManagerClient(httpClient, new EnvironmentProperties(Scheme, Host, Port));
        }

        private DomainClient GetDomainClient(HttpClient httpClient, CoreClient coreClient, string systemId)
        {
            var AppsTask = coreClient.FindInstalledAppBySystemIdAndType(systemId, "domain");
            var Apps = AppsTask.Result;
            if (Apps != null && Apps.Count > 0)
            {
                var DomainApp = Apps[0];
                String Host = DomainApp.Host;
                String Port = DomainApp.Port;
                return new DomainClient(httpClient, new EnvironmentProperties("http", Host, Port));
            }
            else
            {
                throw new PlataformaException("Domain configuration not found.");
            }
        }
        private string GetEnvironmentVariable(string variableName, string defaultValue)
        {
            var EnvironmentVariable = Environment.GetEnvironmentVariable(variableName);
            if (EnvironmentVariable != null)
            {
                return EnvironmentVariable;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}