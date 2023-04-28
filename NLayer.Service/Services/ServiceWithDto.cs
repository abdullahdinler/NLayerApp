using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class ServiceWithDto<TEntity, TDto> : IServiceWithDto<TEntity, TDto> where TEntity : BaseEntity where TDto : class
    {


        private readonly IGenericRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public ServiceWithDto(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var newDto = _mapper.Map<TDto>(entity);
            return CustomResponseDto<TDto>.Success(newDto, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            var entities = await _repository.GetAll().ToListAsync();
            var newDtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return CustomResponseDto<IEnumerable<TDto>>.Success(newDtos, StatusCodes.Status200OK);

        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> expression)
        {
            var entity = await _repository.Where(expression).FirstOrDefaultAsync();
            var newDto = _mapper.Map<IEnumerable<TDto>>(entity);
            return CustomResponseDto<IEnumerable<TDto>>.Success(newDto, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            var anyEntity = await _repository.AnyAsync(expression);
            return CustomResponseDto<bool>.Success(anyEntity, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDto<TDto>> AddAsync(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            TDto newDto = _mapper.Map<TDto>(entity);
            return CustomResponseDto<TDto>.Success(newDto, StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> AddRangeAsync(IEnumerable<TDto> dtos)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(dtos);
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            var newDtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return CustomResponseDto<IEnumerable<TDto>>.Success(newDtos, StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);

        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids)
        {
            var entities =  await _repository.Where(x => ids.Contains(x.Id)).ToListAsync();
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
