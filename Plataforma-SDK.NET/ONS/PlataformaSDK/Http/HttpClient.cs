using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.Http
{
    public class HttpClient
    {
        private System.Net.Http.HttpClient Client;
        public HttpClient()
        {
            Client = new System.Net.Http.HttpClient();
        }
        async public virtual Task<string> Get(string url)
        {
            var response = await Client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine("Get " + url);
            return responseBody;
        }

        async public virtual void Post(string url, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            System.Console.WriteLine("Post " + url);
            response.EnsureSuccessStatusCode();
        }

        async public virtual Task Put(string url, string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(url, content);
            System.Console.WriteLine("Put " + url);
            response.EnsureSuccessStatusCode();
        }

    }
}
