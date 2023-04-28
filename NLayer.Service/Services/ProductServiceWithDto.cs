using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDto> , IProductServiceWithDto
    {
        private readonly IProductRepository _repository;
        public ProductServiceWithDto(IUnitOfWork unitOfWork, IGenericRepository<Product> repository, IMapper mapper, IProductRepository repository1) : base(unitOfWork, repository, mapper)
        {
            _repository = repository1;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _repository.GetProductsWithCategory();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(productsDto, 200);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto updateDto)
        {
            var entity = _mapper.Map<Product>(updateDto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto addDto)
        {
            var entity = _mapper.Map<Product>(addDto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<ProductDto>(entity);
            return CustomResponseDto<ProductDto>.Success(newDto, StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDto<List<ProductDto>>> AddRangeAsync(List<ProductCreateDto> addDtos)
        {
            var entities = _mapper.Map<List<Product>>(addDtos);
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            var newDtos = _mapper.Map<List<ProductDto>>(entities);
            return CustomResponseDto<List<ProductDto>>.Success(newDtos, StatusCodes.Status201Created);
        }
    }
}
