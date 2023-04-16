using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdWithProduct(int categoryId)
        {
            return CreateActionResult(await _service.GetSingleCategoryByIdWithProductsAsync(categoryId));
        }
    }
}
