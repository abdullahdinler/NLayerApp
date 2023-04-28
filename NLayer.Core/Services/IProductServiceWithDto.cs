using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IProductServiceWithDto : IServiceWithDto<Product, ProductDto>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory();
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto updateDto);
        Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto addDto);
        Task<CustomResponseDto<List<ProductDto>>> AddRangeAsync(List<ProductCreateDto> addDtos);
    }
}
