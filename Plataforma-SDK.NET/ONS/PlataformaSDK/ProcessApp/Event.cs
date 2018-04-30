using Newtonsoft.Json.Linq;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class Event
    {
        public JObject Values;

        public Event(JObject values) {
            this.Values = values;
        }

        public string Name() {
            System.Console.WriteLine(Values);
            return (string) Values["name"];
        }
    }
}
