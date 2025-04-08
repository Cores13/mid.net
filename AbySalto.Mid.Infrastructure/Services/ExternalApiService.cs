using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces.Services;
using System.Text.Json;

namespace AbySalto.Mid.Infrastructure.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProducts()
        {
            var response = await _httpClient.GetAsync($"products?limit=300");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var res = JsonSerializer.Deserialize<ApiProductResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            if(res.Total > 300)
            {
                response = await _httpClient.GetAsync($"products?limit={res.Total}");
                response.EnsureSuccessStatusCode();

                json = await response.Content.ReadAsStringAsync();

                res = JsonSerializer.Deserialize<ApiProductResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return res.Products ?? new List<Product>();
        }

        public async Task<int> GetProductsCount()
        {
            var response = await _httpClient.GetAsync($"products?limit=1");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var res = JsonSerializer.Deserialize<ApiProductResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return res.Total;
        }
    }
}
