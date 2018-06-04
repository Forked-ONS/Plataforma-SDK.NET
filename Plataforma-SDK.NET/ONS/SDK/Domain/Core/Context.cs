namespace ONS.SDK.Domain.Core{
    /**
    Esta classe implementa o comportamento do contexto de execução
     */
    public class Context<T> {
        public Event<T> Event {get;set;}

        public DataSet DataSet {get;set;}

        public string InstanceId {get;set;}
    }
}