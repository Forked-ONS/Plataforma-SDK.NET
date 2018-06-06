namespace ONS.SDK.Domain.Core {
    public class Filter {
        public string Map { get; set; }
        public string Entity { get; set; }
        public string Name { get; set; }
        public string Query { get; set; }
        public string ToQueryString () {
            return "";
        }
    }
}