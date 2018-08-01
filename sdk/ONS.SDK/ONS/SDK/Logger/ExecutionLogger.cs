using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ONS.SDK.Configuration;
using ONS.SDK.Context;

namespace ONS.SDK.Logger
{
    public class ExecutionLogger : ILogger
    {
        private readonly string _name;

        private readonly ExecutionLoggerConfiguration _config;

        public ExecutionLogger(string name, ExecutionLoggerConfiguration config)
        {
            _name = name;
            _config = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _config.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) {
                return;
            }

            var executionContext = SDKConfiguration.ServiceProvider.GetService<IExecutionContext>();
            
            if (executionContext != null && executionContext.ExecutionParameter != null 
                && executionContext.ExecutionParameter.Memory != null) {

                executionContext.ExecutionParameter.Memory.AddLog(
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss,fff}] [{eventId.Id}] {logLevel.ToString()} [{_name}]: {formatter(state, exception)}"
                );
            }
        }
    }
}