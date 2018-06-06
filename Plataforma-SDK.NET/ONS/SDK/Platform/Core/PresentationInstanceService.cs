using System.Collections.Generic;
using ONS.SDK.Infra;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.Core
{
    public class PresentationInstanceService : CoreService
    {
        public PresentationInstanceService(CoreConfig config, JsonHttpClient client) : base(config, client, "presentationInstance")
        {
        }

        public List<PresentationInstance> FindByPresentationId(string presentationId){
            var criteria = new Criteria () {
                FilterName = "byPresentationId",
                Parameters = new List<Parameter> ()
                {
                new Parameter ()
                    {
                        Name = "presentationId", Value = presentationId
                    }
                }
            };
            return this.Find<PresentationInstance>(criteria);
        }
    }
}