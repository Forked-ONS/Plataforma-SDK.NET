using Newtonsoft.Json;

namespace ONS.SDK.Domain.ProcessMemmory {
    
    /// <summary>
    /// Dados de reprodução, em caso de execução de reprodução da instância, 
    /// da memória de processamento.
    /// </summary>
    public class Reproduction {

        [JsonProperty("from")]
        public string From;
        
        [JsonProperty("to")]
        public string To;

        public override string ToString() {
            return $"{this.GetType().Name}[From={From}, To={To}]";
        }
    }
}