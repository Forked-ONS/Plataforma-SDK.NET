namespace ONS.PlataformaSDK.Environment
{
    public class EnvironmentProperties
    {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public EnvironmentProperties(string scheme, string host, string port)
        {
            this.Scheme = scheme;
            this.Host = host;
            this.Port = port;
        }

    }
}
