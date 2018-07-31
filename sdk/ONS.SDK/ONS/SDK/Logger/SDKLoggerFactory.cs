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
            var logLevalValidate = _loggerFactory.CreateLogger<SDKLoggerFactory>();
            _loggerFactory.AddProvider(
                new ExecutionLoggerProvider(new ExecutionLoggerConfiguration(logLevalValidate))
            );
        }

        private static void _validateFactory() {
            if (_loggerFactory == null) {
                throw new BadConfigException("LoggerFactory not initialized.");
            }
        }

    }
}