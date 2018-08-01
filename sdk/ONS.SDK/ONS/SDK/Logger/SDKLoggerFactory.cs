using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ONS.SDK.Configuration;
using Microsoft.Extensions.Configuration;

namespace ONS.SDK.Logger
{   
    ///<summary>Classe de fabricação de logger para registros de logs para o sistema.</summary>
    public class SDKLoggerFactory 
    {
        private static readonly string ConfigOperationLogLevel = "OperationLogLevel";

        /// <summary>
        /// Fábrica de loggers do sistema.
        /// </summary>
        private static ILoggerFactory _loggerFactory;

        public static void Init() {

            _loggerFactory = SDKConfiguration.ServiceProvider.GetService<ILoggerFactory>();

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
            var configLogLevel = _getConfigurateDefaultLogLevel();
            _loggerFactory.AddProvider(
                new ExecutionLoggerProvider(new ExecutionLoggerConfiguration(configLogLevel))
            );
        }

        private static LogLevel _getConfigurateDefaultLogLevel() {

            var configuration = SDKConfiguration.ServiceProvider.GetService<IConfiguration>();

            var configLogLevel = LogLevel.None;

            var operLogLovel = configuration[ConfigOperationLogLevel];
            if (!string.IsNullOrEmpty(operLogLovel)) {
                configLogLevel = (LogLevel) Enum.Parse(typeof(LogLevel), operLogLovel);

            } else {
                
                var logLevalValidate = _loggerFactory.CreateLogger<SDKLoggerFactory>();
                var levels = Enum.GetValues(typeof(LogLevel)).Cast<LogLevel>();
                foreach(var itLevel in levels) {
                    if (logLevalValidate.IsEnabled(itLevel)) {
                        configLogLevel = itLevel;
                        break;
                    } 
                }
            }
            return configLogLevel;
        }

        private static void _validateFactory() {
            if (_loggerFactory == null) {
                throw new BadConfigException("LoggerFactory not initialized.");
            }
        }

    }
}