using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Plataforma_SDK.NET.ONS.SDK.Utils
{
    public class SDKStopwatch: IDisposable
    {
        private Stopwatch _stopWatch;

        private ILogger _logger;

        private string _message;
        
        private string _dataIdentifyLog;

        public SDKStopwatch(ILogger logger) {
            this._logger = logger;
        }

        public SDKStopwatch(ILogger logger, string message, params LogValue[] dataIdentifyLog) {
            this._logger = logger;
            Start(message, dataIdentifyLog);
        }

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

        public void Dispose() {
            
            if (this._logger.IsEnabled(LogLevel.Debug)) {
                
                var timeTotal = this._stopWatch.ElapsedMilliseconds;
                this._stopWatch.Stop();

                this._logger.LogDebug(this._buildMessageLog(
                    new LogValue($"Finish: {this._message} / Time[{timeTotal}ms]"),
                    new LogValue(" / Data: ", this._dataIdentifyLog)
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
                if (i > 0) { sb.Append("/ "); }
                
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

    public class LogValue {

        public string Key {get; private set;}

        private Func<object> _funcValue;
        private object _value;

        public object Value {
            get {
                return _funcValue != null ? this._funcValue() : this._value ;
            }
        }

        public LogValue(string key) {
            Key = key;
        }

        public LogValue(string key, Func<object> funcValue) {
            this.Key = key;
            this._funcValue = funcValue;
        }

        public LogValue(string key, object value) {
            this.Key = key;
            this._value = value;
        }
    }
}