using System.Collections.Generic;
using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;

using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.Core
{
    public class BranchService : CoreService
    {
        public BranchService(CoreConfig config, JsonHttpClient client) : base(config, client, "branch")
        {
        }

        public List<Branch> FindBySystemIdAndName(string systemId, string name){
            var criteria = new Criteria () {
                FilterName = "bySystemIdAndName",
                Parameters = new List<Parameter> ()
                {
                new Parameter ()
                    {
                        Name = "systemId", Value = systemId
                    },
                new Parameter ()
                    {
                        Name = "name", Value = name
                    }
                }
            };
            return this.Find<Branch>(criteria);
        }

        public List<Branch> FindBySystemIdAndOwner(string systemId, string owner){
            var criteria = new Criteria () {
                FilterName = "bySystemIdAndOwner",
                Parameters = new List<Parameter> ()
                {
                new Parameter ()
                    {
                        Name = "systemId", Value = systemId
                    },
                new Parameter ()
                    {
                        Name = "owner", Value = owner
                    }
                }
            };
            return this.Find<Branch>(criteria);
        }
    }
}