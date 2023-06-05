using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Api.ApiService;

namespace Tranzact.OnlineStore.Infrastructure.Api.ApiService
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendGetRequestAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string data = await response.Content.ReadAsStringAsync();
            return data;
        }

        public async Task<string> SendPostRequestAsync(string url, string requestData)
        {
            HttpContent content = new StringContent(requestData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);
            string responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }

        public async Task<string> SendPutRequestAsync(string url, string id, string updatedData)
        {
            HttpContent content = new StringContent(updatedData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync($"{url}/{id}", content);
            string responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }

        public async Task<string> SendDeleteRequestAsync(string url, string id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{url}/{id}");
            string responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }
    }
}
