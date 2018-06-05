
/**
Context builder é a classe responsável por montar um objeto de contexto de execução
*/
using ONS.SDK.Domain.Core;
using ONS.SDK.Domain.Services;

namespace ONS.SDK.Context {

    public class ContextBuilder<T> {

        private IProcessMemoryService<T> _processMemory;

        public ContextBuilder()
        {
        }

        public ContextBuilder(IProcessMemoryService<T> processMemory){
            this._processMemory = processMemory;
        }
        public Context<T> Build(string instanceId) {
            return new Context<T>() {Event=new Event<T>(), DataSet= new DataSet()};
        }
    }
}