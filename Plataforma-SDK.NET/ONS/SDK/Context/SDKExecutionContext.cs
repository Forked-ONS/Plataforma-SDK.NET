using Microsoft.Extensions.Configuration;

namespace ONS.SDK.Context {
    public class SDKExecutionContext {

        public SDKExecutionContext(IConfiguration configuration)
        {
            SystemId = configuration.GetValue("SYSTEM_ID","");
            ProcessInstanceId = configuration.GetValue("INSTANCE_ID","");
        }

        public SDKExecutionContext() {}

        public string SystemId { get; private set; }
        public string ProcessInstanceId { get; private set; }
    }
}