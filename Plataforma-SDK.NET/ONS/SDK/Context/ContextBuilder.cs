
/**
Context builder é a classe responsável por montar um objeto de contexto de execução
*/
using ONS.SDK.Domain;
using ONS.SDK.Domain.Services;

namespace ONS.SDK.Context {

    public class ContextBuilder<T> where T: class {

        private IProcessMemoryService<T> _processMemory;

        public ContextBuilder()
        {
        }

        public ContextBuilder(IProcessMemoryService<T> processMemory){
            this._processMemory = processMemory;
        }
        public Context<T> Build(string instanceId) => new Context<T>() {Event=new Event<T>(), DataSet= new DataSet() } ;
    }
}