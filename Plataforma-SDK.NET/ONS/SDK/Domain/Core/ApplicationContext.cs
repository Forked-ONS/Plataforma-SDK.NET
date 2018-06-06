using Microsoft.Extensions.Configuration;

namespace Plataforma_SDK.NET.ONS.SDK.Domain.Core {
    public class ApplicationContext {

        public ApplicationContext(IConfiguration configuration)
        {
            SystemId = configuration.GetValue("SYSTEM_ID","");
            ProcessInstanceId = configuration.GetValue("INSTANCE_ID","");
        }

        public ApplicationContext() {}

        public string SystemId { get; set; }
        public string ProcessInstanceId { get; set; }
    }
}