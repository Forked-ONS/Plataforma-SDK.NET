namespace ONS.SDK.Infra {
    public class BaseServiceConfig {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }

        public string Url => $"{Scheme}://{Host}:{Port}";

    }
}