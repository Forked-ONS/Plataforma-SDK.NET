using Microsoft.Extensions.Logging;

namespace ONS.SDK.Logger
{
    public class ExecutionLoggerConfiguration
    {
        public ILogger LogLevelValidate { get; private set; }

        public ExecutionLoggerConfiguration(ILogger logLevelValidate) {
            LogLevelValidate = logLevelValidate;
        }
        
    }
}