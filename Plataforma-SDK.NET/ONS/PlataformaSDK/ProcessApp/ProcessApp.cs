using System.Threading.Tasks;
using ONS.PlataformaSDK.ProcessMemory;
using Newtonsoft.Json.Linq;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class ProcessApp
    {
        private string ProcessInstanceId;
        private ProcessMemoryClient ProcessMemoryClient;

        public ProcessApp(string processInstanceId, ProcessMemoryClient processMemoryClient)
        { 
            this.ProcessInstanceId = processInstanceId;
            this.ProcessMemoryClient = processMemoryClient;
        }

        public async Task Start()
        {
            Task<string> HeadTask = ProcessMemoryClient.Head(this.ProcessInstanceId);
            var head = await HeadTask;
            System.Console.WriteLine(head);
        }

        public Event ParseEvent(string processMemoryValue) {
            var Object = JObject.Parse(processMemoryValue);
            Event Event = new Event(Object);
            return Event;
        }
    }
}
