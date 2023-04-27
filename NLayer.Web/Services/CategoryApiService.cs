using NLayer.Core.DTOs;

namespace NLayer.Web.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _client;

        public CategoryApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var response = await _client.GetFromJsonAsync<CustomResponseDto<List<CategoryDto>>>("Categories");
            return response.Data;
        }
    }
}
