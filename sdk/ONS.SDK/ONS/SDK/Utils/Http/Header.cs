namespace ONS.SDK.Utils.Http {

    /// <summary>
    /// Representa o header de requisição de protocolo http.
    /// </summary>
    public class Header {

        /// <summary>
        /// Chave do cabeçalho de requisição de protocolo http.
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// Valor do cabeçalho de requisição de protocolo http.
        /// </summary>
        public string Value { get; set; }
    }
}