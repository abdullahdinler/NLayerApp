using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            return CreateActionResult(CustomResponseDto<List<CategoryDto>>.Success(categoriesDto, 200));
        }



        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdWithProduct(int categoryId)
        {
            return CreateActionResult(await _service.GetSingleCategoryByIdWithProductsAsync(categoryId));
        }
    }
}
