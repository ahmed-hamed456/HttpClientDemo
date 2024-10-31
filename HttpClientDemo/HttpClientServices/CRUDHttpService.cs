using Newtonsoft.Json;
using System.Text;

namespace HttpClientDemo.HttpClientServices
{
    public class CRUDHttpService
    {
        private readonly IHttpClientFactory _factory;
        private readonly HttpClient _client;
        public CRUDHttpService(IHttpClientFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient("Posts");
        }
        
        public async Task<List<T>> GetAll<T>()
        {
            var response = await _client.GetAsync(_client.BaseAddress);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = new List<T>();

            if(response.Content.Headers.ContentType.MediaType == "application/json")
            {
                result = JsonConvert.DeserializeObject<List<T>>(content);
            }
            else
            {
                //xml
            }

            return result;
        }

        public async Task<T> GetById<T>(string id)
        {
            var response = await _client.GetAsync(_client.BaseAddress + $"/{id}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = (T) Activator.CreateInstance(typeof(T));

            if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                result = JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                //xml
            }

            return result;
        }

        public async Task<T> Create<T>(T item)
        {
            var itemToSave = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8,"application/json");
            var response = await _client.PostAsync(_client.BaseAddress, itemToSave);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = (T)Activator.CreateInstance(typeof(T));

            if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                result = JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                //xml
            }

            return result;
        }

        public async Task<T> Update<T>(T item)
        {
            var itemToSave = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_client.BaseAddress, itemToSave);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var result = (T)Activator.CreateInstance(typeof(T));

            if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                result = JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                //xml
            }

            return result;
        }

        public async Task Delete<T>(string id)
        {
            var response = await _client.DeleteAsync(_client.BaseAddress + $"/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
