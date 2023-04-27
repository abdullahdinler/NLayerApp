using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Web.Models;
using NLayer.Web.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            
            return View(await _productApiService.GetProductsWithCategoryAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Save()
        {
            var categoryDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromForm] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productDto);
                return RedirectToAction(nameof(Index));
            }

            var categoryDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);
            var categoryDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(productDto);
                return RedirectToAction(nameof(Index));
            }

            var categoryDto = await _categoryApiService.GetAllAsync();
            ViewBag.Categories = new SelectList(categoryDto, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
