namespace Application.Services.Primitives;

using System.Linq.Expressions;
using Application.Entities.Primitives;
using Application.Exceptions;
using Application.Interfaces.Repositories.Primitives;
using Application.Interfaces.Services.Primitives;

/// <summary>
/// Implements <see cref="IBaseService{T}"/> to provide default service-layer behavior
/// for basic CRUD operations, including soft delete, using the repository pattern.
/// </summary>
/// <typeparam name="T">The entity type, which must inherit from <see cref="BaseEntity"/>.</typeparam>
public class BaseService<T>(IBaseRepository<T> repository) : IBaseService<T> where T : BaseEntity
{
    protected readonly IBaseRepository<T> _repo = repository;

    /// <inheritdoc/>
    public Task<T?> GetByIdAsync(Guid id)
        => _repo.GetByIdAsNoTrackingAsync(id);

    /// <inheritdoc/>
    public async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var query = await _repo.GetAllAsNoTracking().ToListAsync();
        return query.Select(_mapper.ToDto);
    }

    /// <inheritdoc/>
    public async Task<PaginatedResult<TDto>> GetPaginatedAsync(int page, int size, Expression<Func<T, bool>>? filter = null)
    {
        var query = _repo.GetAllAsNoTracking();

        if (filter is not null)
            query = query.Where(filter);

        return await query.ToPaginatedResultAsync(_mapper, page, size);
    }

    /// <inheritdoc/>
    public async Task<TDto> CreateAsync(T entity)
    {
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc/>
    public async Task<T> UpdateAsync(T entity)
    {
        _repo.Update(entity);
        await _repo.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc/>
    public async Task SoftDeleteAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException($"{typeof(T).Name} not found");

        _repo.SoftDelete(entity);
        await _repo.SaveChangesAsync();
    }
}