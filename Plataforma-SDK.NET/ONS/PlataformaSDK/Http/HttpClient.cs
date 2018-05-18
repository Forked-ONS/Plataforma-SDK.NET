using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.Http
{

    public class HttpClient
    {
        private System.Net.Http.HttpClient client;
        public HttpClient()
        {
            client = new System.Net.Http.HttpClient();
        }
        async public virtual Task<string> Get(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        async public virtual void Post(string url, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
        }

    }
}
