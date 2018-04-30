using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.Http
{

    public class HttpClient
    {
        private System.Net.Http.HttpClient client;
        public HttpClient() {
            client = new System.Net.Http.HttpClient();
        } 
        async public virtual Task<string> Get(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode(); 
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

    }
}
