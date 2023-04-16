using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    // Product sınıfı için gerekli servisler burada tanımlanır
    // GetAll , GetById , Save , Update , Remove gibi metotlar burada tanımlanır
    // Bu metotlar Product sınıfı için gerekli işlemleri yapar
    // Örnek olarak GetById metodu Product sınıfı için GetByIdAsync metodu çağırır. Ve Id sahip product döner 

    
    public class ProductsController : CustomBaseController
    {
        
        private readonly IService<Product> _service;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IService<Product> service, IMapper mapper, IProductService productService)
        {
            _service = service;
            _mapper = mapper;
            _productService = productService;
        }

        //Get api/product/
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(productsDtos, 200));
        }

        //Get api/product/GetProductWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductWithCategory()
        {
            return CreateActionResult(await _productService.GetProductsWithCategory());
        }

        //Get api/product/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(productDto, 200));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var newProduct = await _service.AddAsync(product);
            var newProductDto = _mapper.Map<ProductDto>(newProduct);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(newProductDto, 201));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _service.UpdateAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
