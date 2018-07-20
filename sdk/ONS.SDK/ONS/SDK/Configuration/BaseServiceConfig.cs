namespace ONS.SDK.Configuration {

    /// <summary>
    /// Classe base para representar as configurações de serviços da plataforma.
    /// </summary>
    public class BaseServiceConfig {

        /// <summary>
        /// Esquema da url de acesso ao serviço.
        /// </summary>
        public string Scheme { get; set; }
        
        /// <summary>
        /// Host da url de acesso ao serviço.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Porta da url de acesso ao serviço.
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Url de acesso ao serviço.
        /// </summary>
        public string Url => $"{Scheme}://{Host}:{Port}";

    }
}