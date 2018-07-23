using System;
using System.Collections.Generic;
using System.Linq;

namespace ONS.SDK.Services.Domain {
    
    /// <summary>
    /// Representa o filtro de consulta do domínio.
    /// </summary>
    public class Filter {

        /// <summary>
        /// Construtor.
        /// </summary>
        public Filter() {
            Parameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// Nome do mapa ao qual pertence o filtro da entidade.
        /// </summary>
        public string Map { get; set; }
        
        /// <summary>
        /// Nome da entidade do mapa ao qual pertence o filtro.
        /// </summary>
        public string Entity { get; set; }
        
        /// <summary>
        /// Nome do filtro.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Parâmetros para aplicação do filtro e execução de consulta.
        /// </summary>
        public IDictionary<string, object> Parameters { get; set; }

    }
}