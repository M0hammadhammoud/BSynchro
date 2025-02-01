using Common.Utitlities.Contracts;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Common.Utitlities.Helpers
{
    //Http helper is used to perform http calls between microservices or external services
    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly HttpClient _httpClient;

        public HttpClientHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void SetDefaultHeaders(Dictionary<string, string> headers)
        {
            _httpClient.DefaultRequestHeaders.Clear();

            foreach (var header in headers)
            {
                if (header.Key != "Content-Type")
                {
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                else
                {
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header.Value));
                }
            }
        }

        public async Task<T> GetAsync<T>(string resource)
        {
            var response = await _httpClient.GetAsync(resource);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<T1> PostAsync<T, T1>(string resource, T requestBody)
        {
            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(resource, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(responseJson);
        }

        public async Task<T1> PutAsync<T, T1>(string resource, T requestBody)
        {
            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(resource, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(responseJson);
        }

        public async Task DeleteAsync(string resource)
        {
            var response = await _httpClient.DeleteAsync(resource);
            response.EnsureSuccessStatusCode();
        }
    }
}
