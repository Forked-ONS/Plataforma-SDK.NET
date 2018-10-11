using System;
using Newtonsoft.Json;

namespace ONS.SDK.Domain.Base {

    /// <summary>
    /// Tipo base para entidade mapeadas de domínio.
    /// </summary>
    public class Model {

        /// <summary>
        /// Construtor.
        /// </summary>
        public Model() {}

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="generateId">Indica se a classe tera o identificador gerado automaticamente.</param>
        public Model(bool generateId) {
            this.Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Identificador da entidade.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Metadados de persistência.
        /// </summary>
        [JsonProperty("_metadata")]
        public Metadata _Metadata { get; set; }
    }
}