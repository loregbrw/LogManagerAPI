namespace Application.Services.Primitives;

using System.Linq.Expressions;
using Application.Entities.Primitives;
using Application.Exceptions;
using Application.Interfaces.Repositories.Primitives;
using Application.Interfaces.Services.Domain.Primitives;
using Application.Mappers.Primitives;
using Application.Models.Entities.Primitives;
using Application.Models.Pagination;
using Microsoft.EntityFrameworkCore;

public class BaseService<T, TDto>(IBaseRepository<T> repository, IEntityMapper<T, TDto> mapper) : IBaseService<T, TDto> where T : BaseEntity where TDto : BaseDto
{
    protected readonly IBaseRepository<T> _repo = repository;

    public async Task<TDto?> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsNoTrackingAsync(id);
        return entity is null ? null : _mapper.ToDto(entity);
    }

    public async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var query = await _repo.GetAllAsNoTracking().ToListAsync();
        return query.Select(_mapper.ToDto);
    }

    public async Task<PaginatedResult<TDto>> GetPaginatedAsync(int page, int size, Expression<Func<T, bool>>? filter = null)
    {
        var query = _repo.GetAllAsNoTracking();

        if (filter is not null)
            query = query.Where(filter);

        return await query.ToPaginatedResultAsync(_mapper, page, size);
    }

    public async Task<TDto> CreateAsync(T entity)
    {
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();
        return entity;
    }

    public async Task<TDto> UpdateAsync(T entity)
    {
        _repo.Update(entity);
        await _repo.SaveChangesAsync();
        return entity;
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"{typeof(T).Name} not found");

        _repo.SoftDelete(entity);
        await _repo.SaveChangesAsync();
    }
}