using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace ONS.SDK.Logger
{
    public class ExecutionLoggerProvider : ILoggerProvider
    {
        private readonly ExecutionLoggerConfiguration _config;
    
        private readonly ConcurrentDictionary<string, ExecutionLogger> _loggers = new ConcurrentDictionary<string, ExecutionLogger>();

        public ExecutionLoggerProvider(ExecutionLoggerConfiguration config)
        {
            _config = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new ExecutionLogger(name, _config));
        }
        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}