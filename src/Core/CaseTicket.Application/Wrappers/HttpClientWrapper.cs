using CaseTicket.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CaseTicket.Application.Wrappers
{
    public class HttpClientWrapper:IHttpClientWrapper
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public HttpClientWrapper(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public void AddBasicAuthentication()
        {
            //olası bir iki kere ekleme durumu engellendi if else ile uzatmak istemedim
            _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Add("Authorization", _configuration["OBiletAPI:BasicAuth"]);
        }

        public async Task<string> GetStringAsyncWrapper(string url)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
                else
                    throw new Exception($"Error Code {response.StatusCode}");
            }
            catch (Exception e)
            {
                throw new Exception($"GET Request Error => {e.Message}");
            }
        }

        public async Task<string> PostAsyncWrapper(string url, string jsonContent)
        {
            try
            {
                //burda application/json parametrik yapılabilirdi ancak case özelinde yeterli 
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();
                else
                    throw new Exception($"Error Code {response.StatusCode}");
            }
            catch (Exception e)
            {
                throw new Exception($"POST Request Error => {e.Message}");
            }
        }
    }
}
