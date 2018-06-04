namespace ONS.SDK.Domain.Core
{
    public class Memory<T>
    {
        public Event<T> Event { get; set; }
        public string ProcessId { get; set; }
        public string SystemId { get; set; }
        public string InstanceId { get; set; }
        public string EventOut { get; set; }
        public bool Commit { get; set; }
        public Map Map { get; set; }
        public DataSet DataSet { get; set; }
    }
}
