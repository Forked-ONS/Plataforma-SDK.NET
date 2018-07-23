using Newtonsoft.Json;

namespace ONS.SDK.Domain.ProcessMemmory {
    
    /// <summary>
    /// Dados de reprocessamento, em caso de execução de reprocesamento da instância, 
    /// da memória de processamento.
    /// </summary>
    public class Reprocess {
        
        [JsonProperty("from")]
        public string From;

        [JsonProperty("to")]
        public string To;

        public override string ToString() {
            return $"{this.GetType().Name}[From={From}, To={To}]";
        }
    }
}