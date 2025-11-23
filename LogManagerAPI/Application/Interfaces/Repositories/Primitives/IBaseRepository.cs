namespace Application.Interfaces.Repositories.Primitives;

using Application.Entities.Primitives;

public interface IBaseRepository<T> where T : BaseEntity
{

    IQueryable<T> GetAll();

    IQueryable<T> GetAllAsNoTracking();

    IQueryable<T> GetAllIncludingDeleted();

    IQueryable<T> GetAllIncludingDeletedAsNoTracking();

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
   
    Task<T?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    
    Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
   
    void Update(T entity);

    void Update(IEnumerable<T> entities);

    public void SoftDelete(T entity);

    public void SoftDelete(IEnumerable<T> entities);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}