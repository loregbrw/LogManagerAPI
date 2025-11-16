namespace Infrastructure.Repositories.Primitives;

using Application.Entities.Primitives;
using Application.Interfaces.Repositories.Primitives;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

public class BaseRepository<T>(LogManagerDbContext context) : IBaseRepository<T> where T : BaseEntity
{
    protected readonly LogManagerDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public IQueryable<T> GetAll() => _dbSet;

    public IQueryable<T> GetAllAsNoTracking() => _dbSet.AsNoTracking();

    public IQueryable<T> GetAllIncludingDeleted() => _dbSet.IgnoreQueryFilters();

    public IQueryable<T> GetAllIncludingDeletedAsNoTracking() => _dbSet.IgnoreQueryFilters().AsNoTracking();

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) 
        => _dbSet.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    public Task<T?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default)
        => _dbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    public Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => _dbSet.AddAsync(entity, cancellationToken).AsTask();

    public Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        => _dbSet.AddRangeAsync(entities, cancellationToken);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Update(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);

    public void SoftDelete(T entity) => _dbSet.Remove(entity);

    public void SoftDelete(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}