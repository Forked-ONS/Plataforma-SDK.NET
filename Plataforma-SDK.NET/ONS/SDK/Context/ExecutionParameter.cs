using System;
using Microsoft.Extensions.Configuration;
using ONS.SDK.Configuration;

namespace ONS.SDK.Context {

    public class ExecutionParameter 
    {
        public string Branch { get; internal set; }
        public DateTime ReferenceDate { get; internal set; }

        public string InstanceId { get; internal set; }
        
        public ExecutionParameter()
        {
            this.Branch = SDKConstants.BranchMaster;
            this.ReferenceDate = DateTime.Now;
        }
    }
}