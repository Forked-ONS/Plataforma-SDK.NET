using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ONS.SDK.Domain.ProcessMemmory {

    /// <summary>
    /// Representa a própria memória de processamento.
    /// </summary>
    public class Memory {

        /// <summary>
        /// Evento original que disparou a execução do processamento, 
        /// ao qual se refere a memória.
        /// </summary>
        [JsonProperty("event")]
        public MemoryEvent Event { get; set; }

        /// <summary>
        /// Identificador do processo em execução.
        /// </summary>
        [JsonProperty("processId")]
        public string ProcessId { get; set; }
        
        /// <summary>
        /// Identificador do Sistema que detém o processo.
        /// </summary>
        /// <value></value>
        [JsonProperty("systemId")]
        public string SystemId { get; set; }

        /// <summary>
        /// Identificador da instância de execução que se refere a memória.
        /// </summary>
        [JsonProperty("instanceId")]
        public string InstanceId { get; set; }
        
        /// <summary>
        /// Evento que será disparado ao final da execução.
        /// </summary>
        [JsonProperty("eventOut")]
        public string EventOut { get; set; }
        
        /// <summary>
        /// Dados de criação de branch.
        /// </summary>
        [JsonProperty("fork")]
        public Fork Fork { get; set; }

        /// <summary>
        /// Indica se os dados serão confirmados ao final do processamento.
        /// </summary>
        [JsonProperty("commit")]
        public bool Commit { get; set; }
        
        /// <summary>
        /// Informações do Mapa de entidades do processo.
        /// </summary>
        [JsonProperty("map")]
        public ProcessMap Map { get; set; }
        
        /// <summary>
        /// Dados de entidade do contexto de persistência que estão na memória.
        /// </summary>
        [JsonProperty("dataset")]
        public DataSetMap DataSet { get; set; }

        /// <summary>
        /// Informações de logs registrados durante o processamento da operação.
        /// </summary>
        [JsonProperty("logs")]
        public IList<string> Logs { get; set; }

        /// <summary>
        /// Adiciona um registro de log na memória de processamento 
        /// para a execução da operação.
        /// </summary>
        /// <param name="logText">Conteúdo para registro de log.</param>
        public void AddLog(string logText) {
            if (Logs == null) {
                Logs = new List<string>();
            }
            Logs.Add(logText);
        }
    }
}