using ONS.SDK.Context;

namespace ONS.SDK.Test.Web.Process
{
    public class AlteraCenarioPayload: IPayload
    {
        public string Id {get;set;}

        public string Name {get;set;}

        public int Balance {get;set;}

        public override string ToString() {
            return string.Format("[{0}] {{ Id={1}, Name={2}, Balance={3} }}",
                GetType().Name, Id, Name, Balance
            );
        }
    }
}