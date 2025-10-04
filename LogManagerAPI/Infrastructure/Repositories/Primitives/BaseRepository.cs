namespace Infrastructure.Repositories.Primitives;

using Application.Entities.Primitives;
using Application.Interfaces.Providers;
using Application.Interfaces.Repositories.Primitives;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

public class BaseRepository<T>(LogManagerDbContext context, IDateTimeProvider dateTimeProvider) : IBaseRepository<T> where T : BaseEntity
{
    protected readonly LogManagerDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();
    protected readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public IQueryable<T> GetAll() => _dbSet;

    public IQueryable<T> GetAllAsNoTracking() => _dbSet.AsNoTracking();

    public IQueryable<T> GetAllIncludingDeleted() => _dbSet.IgnoreQueryFilters();

    public IQueryable<T> GetAllIncludingDeletedAsNoTracking() => _dbSet.IgnoreQueryFilters().AsNoTracking();

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _dbSet.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    public Task<T?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default) =>
        _dbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    public Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedAt = _dateTimeProvider.UtcNow;
        return _dbSet.AddAsync(entity, cancellationToken).AsTask();
    }

    public Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var now = _dateTimeProvider.UtcNow;

        foreach (var entity in entities)
            entity.CreatedAt = now;

        return _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public void Update(T entity)
    {
        entity.UpdatedAt = _dateTimeProvider.UtcNow;
        _dbSet.Update(entity);
    }

    public void Update(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            entity.UpdatedAt = _dateTimeProvider.UtcNow;

        _dbSet.UpdateRange(entities);
    }

    public void SoftDelete(T entity)
    {
        entity.DeletedAt = _dateTimeProvider.UtcNow;
        _dbSet.Update(entity);
    }

    public void SoftDelete(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            entity.DeletedAt = _dateTimeProvider.UtcNow;

        _dbSet.UpdateRange(entities);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}