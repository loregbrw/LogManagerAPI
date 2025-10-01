namespace Infrastructure.Repositories.Primitives;

using Application.Entities.Primitives;
using Application.Interfaces.Providers;
using Application.Interfaces.Repositories.Primitives;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

/// <summary>
/// Implements <see cref="IBaseRepository{T}"/> to provide generic data access operations
/// for entities inheriting from <see cref="BaseEntity"/>. Supports tracking, no-tracking,
/// soft delete, and audit timestamp management.
/// </summary>
/// <typeparam name="T">The entity type inheriting from <see cref="BaseEntity"/>.</typeparam>
/// <remarks>
/// This repository uses <see cref="LogManagerDbContext"/> and <see cref="IDateTimeProvider"/> 
/// to manage data persistence and auditing.
/// </remarks>
public class BaseRepository<T>(LogManagerDbContext context, IDateTimeProvider dateTimeProvider) : IBaseRepository<T> where T : BaseEntity
{
    protected readonly LogManagerDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();
    protected readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    /// <inheritdoc/>
    public IQueryable<T> GetAll() => _dbSet;

    /// <inheritdoc/>
    public IQueryable<T> GetAllAsNoTracking() => _dbSet.AsNoTracking();

    /// <inheritdoc/>
    public IQueryable<T> GetAllIncludingDeleted() => _dbSet.IgnoreQueryFilters();

    /// <inheritdoc/>
    public IQueryable<T> GetAllIncludingDeletedAsNoTracking() => _dbSet.IgnoreQueryFilters().AsNoTracking();

    /// <inheritdoc/>
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        _dbSet.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    /// <inheritdoc/>
    public Task<T?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default) =>
        _dbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    /// <inheritdoc/>
    public Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedAt = _dateTimeProvider.UtcNow;
        return _dbSet.AddAsync(entity, cancellationToken).AsTask();
    }

    /// <inheritdoc/>
    public Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var now = _dateTimeProvider.UtcNow;

        foreach (var entity in entities)
            entity.CreatedAt = now;

        return _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    /// <inheritdoc/>
    public void Update(T entity)
    {
        entity.UpdatedAt = _dateTimeProvider.UtcNow;
        _dbSet.Update(entity);
    }

    /// <inheritdoc/>
    public void Update(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            entity.UpdatedAt = _dateTimeProvider.UtcNow;

        _dbSet.UpdateRange(entities);
    }

    /// <inheritdoc/>
    public void SoftDelete(T entity)
    {
        entity.DeletedAt = _dateTimeProvider.UtcNow;
        _dbSet.Update(entity);
    }

    /// <inheritdoc/>
    public void SoftDelete(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            entity.DeletedAt = _dateTimeProvider.UtcNow;

        _dbSet.UpdateRange(entities);
    }

    /// <inheritdoc/>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}