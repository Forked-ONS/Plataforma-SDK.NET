using System;
using Newtonsoft.Json;

namespace ONS.SDK.Domain.Base {
    
    /// <summary>
    /// Define metadados de entidade de contexto, persistidas em domínio.
    /// </summary>
    public class Metadata {
        
        /// <summary>
        /// Nome do Branch ao qual pertence a entidade.
        /// </summary>
        [JsonProperty("branch")]
        public string Branch { get; set; }
        
        /// <summary>
        /// Identificador da instância que persistiu a entidade.
        /// </summary>
        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }
        
        /// <summary>
        /// Nome do tipo de entidade do mapa que corresponde a entidade.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
        
        /// <summary>
        /// Indica a origem da entidade.
        /// </summary>
        [JsonProperty("origin")]
        public string Origin { get; set; }
        
        /// <summary>
        /// Data de modificação da entidade.
        /// </summary>
        [JsonProperty("modified_at")]
        public DateTime? ModifiedAt { get; set; }
        
        /// <summary>
        /// Tipo de alteração que a entidade sofreu.
        /// Vide tipo ChangeTrack.
        /// </summary>
        [JsonProperty("changeTrack")]
        public string ChangeTrack { get; set; }

        /// <summary>
        /// Versão do registro de entidade.
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

    }
}