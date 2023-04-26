using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, ICategoryService categoryService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetProductsWithCategory());
        }

        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromForm] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productDto);
                await _productService.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name");
            return View();
        }

        [HttpGet]
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            var categories = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name", product.CategoryId);
            return View(productDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productDto);
                await _productService.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);
            return RedirectToAction(nameof(Index));
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}
