using Microsoft.Extensions.Logging;

namespace ONS.SDK.Logger
{
    public class ExecutionLoggerConfiguration
    {
        public LogLevel LogLevel { get; private set; }

        public ExecutionLoggerConfiguration(LogLevel logLevel) {
            LogLevel = logLevel;
        }

        public bool IsEnabled(LogLevel logLevel) {
            return logLevel >= LogLevel;
        }
        
    }
}