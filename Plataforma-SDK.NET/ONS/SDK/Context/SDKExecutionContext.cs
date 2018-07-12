using System;
using Microsoft.Extensions.Configuration;

namespace ONS.SDK.Context {

    public class SDKExecutionContext: IExecutionContext 
    {
        public string SystemId { get; private set; }
        public string ProcessId { get; private set; }
        public string ProcessInstanceId { get; private set; }

        public SDKExecutionContext(IConfiguration configuration)
        {
            SystemId = configuration.GetValue("SYSTEM_ID","");
            ProcessId = configuration.GetValue("PROCESS_ID","");
            ProcessInstanceId = configuration.GetValue("INSTANCE_ID","");
        }

        public bool IsExecutionWeb { 
            get {
                return string.IsNullOrEmpty(ProcessInstanceId);
            }
        }

        public bool IsExecutionConsole { 
            get {
                return !string.IsNullOrEmpty(ProcessInstanceId);
            }
        }

        [ThreadStatic]
        private static ExecutionParameter _executionParameter;

        public ExecutionParameter ExecutionParameter {
            get {
                return _executionParameter;
            }
            set {
                _executionParameter = value;
            }
        }

        public IExecutionContext Begin(ExecutionParameter executionParameter = null) {
            ExecutionParameter = executionParameter != null ? executionParameter: new ExecutionParameter();
            return this;
        }

        public void Dispose()
        {
            ExecutionParameter = null;
        }

        public override string ToString() {
            return $"[{this.GetType().Name}] {{ SystemId={SystemId}, ProcessId={ProcessId}, "+
                $"ProcessInstanceId={ProcessInstanceId}, ExecutionParameter={ExecutionParameter} }}";
        }
    }
}