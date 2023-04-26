using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Caching
{


    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "cacheProduct";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memorycache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;


        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memorycache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memorycache = memorycache;
            _repository = repository;
            _unitOfWork = unitOfWork;

            if (!_memorycache.TryGetValue(CacheProductKey, out _))
            {
                _memorycache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
            }
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memorycache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product)}({id}) not found");
            }

            return Task.FromResult(product);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memorycache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memorycache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            return Task.FromResult(_memorycache.Get<List<Product>>(CacheProductKey).Any());
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entities;
        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

        }

        public async Task RemoveAsync(Product entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public Task<List<ProductWithCategoryDto>> GetProductsWithCategory()
        {
            var products = _memorycache.Get<IEnumerable<Product>>(CacheProductKey);
            var productWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return Task.FromResult(productWithCategoryDto);
        }


        public async Task CacheAllProductsAsync()
        {
            _memorycache.Set(CacheProductKey, await _repository.GetAll().ToListAsync());
        }
    }
}
