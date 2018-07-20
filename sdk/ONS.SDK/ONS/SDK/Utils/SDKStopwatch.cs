using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging;

namespace ONS.SDK.Utils
{
    /// <summary>
    /// Representa um stopwatch para o SDK, com modelo de log de informações de tempo de execução
    /// de um bloco de código. Onde é monitorado e logado o tempo de execução de um trecho de código. 
    /// </summary>
    public class SDKStopwatch: IDisposable
    {
        private Stopwatch _stopWatch;

        private ILogger _logger;

        private string _message;
        
        private string _dataIdentifyLog;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="logger">Parâmetro do construtor.</param>
        public SDKStopwatch(ILogger logger) {
            this._logger = logger;
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="logger">Parâmetro do construtor.</param>
        /// <param name="message">Parâmetro do construtor.</param>
        /// <param name="dataIdentifyLog">Parâmetro do construtor.</param>
        public SDKStopwatch(ILogger logger, string message, params LogValue[] dataIdentifyLog) {
            this._logger = logger;
            Start(message, dataIdentifyLog);
        }

        /// <summary>
        /// Inicia a monitoração de um trecho de código, com log e registro de tempo de execução.
        /// </summary>
        /// <param name="message">Mensagem de log registrado ao iniciar um trecho de código.</param>
        /// <param name="dataIdentifyLog">Dados para serem logados ao iniciar o trecho de código.</param>
        /// <returns>Próprio gerenciador de tempo de execução.</returns>
        public SDKStopwatch Start(string message, params LogValue[] dataIdentifyLog) {
            
            if (this._logger.IsEnabled(LogLevel.Debug)) {
                
                this._message = message;
                this._dataIdentifyLog = this._convertString(dataIdentifyLog);

                this._stopWatch = new Stopwatch();
                this._stopWatch.Start();
                
                this._logger.LogDebug(this._buildMessageLog(
                    new LogValue($"Start: {this._message}"),
                    new LogValue(" / Data: ", this._dataIdentifyLog)
                ));
            }

            return this;
        }

        /// <summary>
        /// Registra log durante a monitoração de tempo de execução.
        /// </summary>
        /// <param name="messageLog">Mensagem que deve ser logada.</param>
        /// <param name="dataLog">Dados para serem logados em conjunto com a mensagem.</param>
        public void Log(string messageLog, params LogValue[] dataLog) {
            
            if (this._logger.IsEnabled(LogLevel.Debug)) {
                
                var strDataLog = this._convertString(dataLog);
                var timeTotal = this._stopWatch.ElapsedMilliseconds;

                this._logger.LogDebug(this._buildMessageLog(
                    new LogValue($"Executed: {messageLog} / Time[{timeTotal}ms]"),
                    new LogValue(" / DataID: ", this._dataIdentifyLog),
                    new LogValue(" / DataLog: ", strDataLog)
                ));
            }   
        }

        /// <summary>
        /// Finaliza a monitoração do tempo de execução de um trecho de código, 
        /// e registra log com os dados e tempos de execução.
        /// </summary>
        public void Dispose() {
            
            if (this._logger.IsEnabled(LogLevel.Debug)) {
                
                var timeTotal = this._stopWatch.ElapsedMilliseconds;
                this._stopWatch.Stop();

                this._logger.LogDebug(this._buildMessageLog(
                    new LogValue($"Finish: {this._message} / Time[{timeTotal}ms]"),
                    new LogValue(" / DataID: ", this._dataIdentifyLog)
                ));

                this._stopWatch = null;
                this._logger = null;
                this._message = null;
                this._dataIdentifyLog = null;
            }

        }

        private string _buildMessageLog(params LogValue[] contentsMsg) {
            
            if (contentsMsg == null) {
                return "";
            }

            var sb = new StringBuilder();
            
            for (int i = 0; i < contentsMsg.Length; i++)
            {   
                var keyValue = contentsMsg[i];
                var valueStr = "" + keyValue.Value;
                if (keyValue.Value != null && !string.IsNullOrEmpty(valueStr)) {
                    sb.Append(keyValue.Key).Append(valueStr);
                } else {
                    sb.Append(keyValue.Key);
                }
            }

            return sb.ToString();
        }

        private string _convertString(params LogValue[] dataIdentifyLog) {

            if (dataIdentifyLog == null) {
                return "";
            }
            var sb = new StringBuilder();
            for (int i = 0; i < dataIdentifyLog.Length; i++)
            {
                if (i > 0) {
                    sb.Append(", ");    
                }
                var keyValue = dataIdentifyLog[i];
                sb.Append(keyValue.Key).Append(keyValue.Value);    
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// Representa um valor de log a ser registrado.
    /// </summary>
    public class LogValue {

        /// <summary>
        /// Chave que representa um valor a ser logado.
        /// </summary>
        public string Key {get; private set;}

        private Func<object> _funcValue;
        private object _value;

        /// <summary>
        /// Valor a ser logado.
        /// </summary>
        public object Value {
            get {
                return _funcValue != null ? this._funcValue() : this._value ;
            }
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="key">Parâmetro do construtor.</param>
        public LogValue(string key) {
            Key = key;
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="key">Parâmetro do construtor.</param>
        /// <param name="funcValue">Parâmetro do construtor.</param>
        public LogValue(string key, Func<object> funcValue) {
            this.Key = key;
            this._funcValue = funcValue;
        }

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="key">Parâmetro do construtor.</param>
        /// <param name="value">Parâmetro do construtor.</param>
        public LogValue(string key, object value) {
            this.Key = key;
            this._value = value;
        }
    }
}