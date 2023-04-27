using System.Formats.Asn1;
using System.Net.Http.Json;
using NLayer.Core.DTOs;

namespace NLayer.Web.Services
{
    #region Info

    // Burada Product sınıfı için gerekli servisler tanımlanır. 
    // GetAll , GetById , Save , Update , Remove gibi metotlar burada tanımlanır
    // Bu metotlar Product sınıfı için gerekli işlemleri yapar
    // Örnek olarak GetById metodu Product sınıfı için GetByIdAsync metodu çağırır. Ve Id sahip product döner

    #endregion
    public class ProductApiService
    {
        private readonly HttpClient _client;

        public ProductApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var response = await 
                _client.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>(
                    "Products/GetProductWithCategory");
            return response.Data;
        }

        public async Task<ProductDto> SaveAsync(ProductDto product)
        {
            var response = await _client.PostAsJsonAsync("products", product);
            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();
            return result.Data;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _client.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"Products/{id}");
            return response.Data;
        }

        public async Task<bool> UpdateAsync(ProductDto product)
        {
            var response = await _client.PutAsJsonAsync("products", product);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _client.DeleteAsync($"products/{id}");
            return response.IsSuccessStatusCode;
        }

      
    }
}
