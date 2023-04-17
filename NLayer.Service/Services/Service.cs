﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;

namespace NLayer.Service.Services
{

    #region Info

    // Core katmanında yazdığımız IService interface'ini implemente ediyoruz. 
    // Bu sayede Core katmanındaki işlemleri kullanabiliyoruz.
    // Generic bir yapı oluştururken, bu yapıda kullanacağımız repository ve unit of work interface'lerini de generic bir yapı oluşturuyoruz.
    // Bu sayede herhangi bir tablo için bu servisi kullanabiliriz.

    #endregion

    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasEntity = await _repository.GetByIdAsync(id);
            if (hasEntity == null)
            {
                throw new ClientSideException($"{typeof(T).Name}({id}) not found");
            }

            return hasEntity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
        }
    }
}
