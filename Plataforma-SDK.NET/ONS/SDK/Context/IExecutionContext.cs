using System;
using Microsoft.Extensions.Configuration;

namespace ONS.SDK.Context {
    
    public interface IExecutionContext: IDisposable
    {
        bool IsExecutionWeb {get;}

        bool IsExecutionConsole {get;}

        string SystemId {get;}
        
        string ProcessId {get;}
        
        string ProcessInstanceId {get;}   

        ExecutionParameter  ExecutionParameter {get;set;}

        IExecutionContext Begin(ExecutionParameter executionParameter);
    }
    
}