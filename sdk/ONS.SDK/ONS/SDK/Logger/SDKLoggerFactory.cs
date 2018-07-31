using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using ONS.SDK.Configuration;

namespace ONS.SDK.Logger
{   
    ///<summary>Classe de fabricação de logger para registros de logs para o sistema.</summary>
    public class SDKLoggerFactory 
    {
        /// <summary>
        /// Fábrica de loggers do sistema.
        /// </summary>
        private static ILoggerFactory _loggerFactory;

        public static void Init(ILoggerFactory loggerFactory) {
            _loggerFactory = loggerFactory;

            _addSDKProvider();
        }

        ///<summary>Método para obter o Logger para um tipo informado, para registro de logs.</summary>
        ///<returns>Logger para o tipo informado.</returns>
        public static ILogger<T> Get<T>() {
            _validateFactory();
            return _loggerFactory.CreateLogger<T>();
        }

        private static void _addSDKProvider() 
        {
            _loggerFactory.AddProvider(new ColoredConsoleLoggerProvider(new ColoredConsoleLoggerConfiguration
            {
                LogLevel = LogLevel.Information,
                Color = ConsoleColor.Blue
            }));
            _loggerFactory.AddProvider(new ColoredConsoleLoggerProvider(new ColoredConsoleLoggerConfiguration
            {
                LogLevel = LogLevel.Debug,
                //Color = ConsoleColor.Gray
                Color = ConsoleColor.Yellow
            }));
            _loggerFactory.AddProvider(new ColoredConsoleLoggerProvider(new ColoredConsoleLoggerConfiguration
            {
                LogLevel = LogLevel.Trace,
                //Color = ConsoleColor.Gray
                Color = ConsoleColor.Yellow
            }));
        }

        private static void _validateFactory() {
            if (_loggerFactory == null) {
                throw new BadConfigException("LoggerFactory not initialized.");
            }
        }

    }

    public class ColoredConsoleLoggerProvider : ILoggerProvider
  {
    private readonly ColoredConsoleLoggerConfiguration _config;
    private readonly ConcurrentDictionary<string, ColoredConsoleLogger> _loggers = new ConcurrentDictionary<string, ColoredConsoleLogger>();

    public ColoredConsoleLoggerProvider(ColoredConsoleLoggerConfiguration config)
    {
      _config = config;
    }

    public ILogger CreateLogger(string categoryName)
    {
      return _loggers.GetOrAdd(categoryName, name => new ColoredConsoleLogger(name, _config));
    }

    public void Dispose()
    {
      _loggers.Clear();
    }
  }

  public class ColoredConsoleLogger : ILogger
{
  private readonly string _name;
  private readonly ColoredConsoleLoggerConfiguration _config;

  public ColoredConsoleLogger(string name, ColoredConsoleLoggerConfiguration config)
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
    return logLevel == _config.LogLevel;
  } 

  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
  {
    if (!IsEnabled(logLevel))
    {
      return;
    }

    if (_config.EventId == 0 || _config.EventId == eventId.Id)
    {
      var color = Console.ForegroundColor;
      Console.ForegroundColor = _config.Color;
      Console.WriteLine($"{logLevel.ToString()} - {eventId.Id} - {_name} - {formatter(state, exception)}");
      Console.ForegroundColor = color;
    }
  }
}

    public class ColoredConsoleLoggerConfiguration
{
  public LogLevel LogLevel { get; set; } = LogLevel.Warning;
  public int EventId { get; set; } = 0;
  public ConsoleColor Color { get; set; } = ConsoleColor.Yellow;
}

}