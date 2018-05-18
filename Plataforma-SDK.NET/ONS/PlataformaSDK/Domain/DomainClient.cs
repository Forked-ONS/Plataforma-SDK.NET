using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.Environment;
using ONS.PlataformaSDK.Http;

namespace ONS.PlataformaSDK.Domain
{
    public class DomainClient
    {
        private HttpClient HttpClient;
        private EnvironmentProperties DomainEnvironmentProperties;

        public DomainClient() {}

        public DomainClient(HttpClient httpClient, EnvironmentProperties domainEnvironmentProperties)
        {
            this.HttpClient = httpClient;
            this.DomainEnvironmentProperties = domainEnvironmentProperties;
        }

        public virtual async Task<List<T>> FindByFilterAsync<T>(EntityFilter entityFilter, Filter filter)
        {
            StringBuilder UrlBuilder;
            if ("all".Equals(filter.Name))
            {
                UrlBuilder = new StringBuilder($"{DomainEnvironmentProperties.Scheme}://{DomainEnvironmentProperties.Host}:{DomainEnvironmentProperties.Port}" +
                    $"/{entityFilter.MapName}/{entityFilter.EntityName}");
            }
            else
            {
                UrlBuilder = new StringBuilder($"{DomainEnvironmentProperties.Scheme}://{DomainEnvironmentProperties.Host}:{DomainEnvironmentProperties.Port}" +
                    $"/{entityFilter.MapName}/{entityFilter.EntityName}?filter={filter.Name}");
            }

            foreach (KeyValuePair<string, object> item in filter.Parameters)
            {
                UrlBuilder.Append($"&{item.Key}={item.Value}");
            }
            var EntityStrTask = HttpClient.Get(UrlBuilder.ToString());
            var EntityTask = await EntityStrTask;
            return JsonConvert.DeserializeObject<List<T>>(EntityTask);
        }
    }
}