namespace Application.Interfaces.Services.Primitives;

using Application.Entities.Primitives;
using Application.Models.Entities.Primitives;
using Application.Models.Pagination;

/// <summary>
/// Defines basic CRUD operations for services working with entities of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The entity type, which must inherit from <see cref="BaseEntity"/>.</typeparam>
public interface IBaseService<T, TDto> where T : BaseEntity where TDto : BaseDto
{
    /// <summary>
    /// Retrieves an entity by its ID, or <c>null</c> if not found.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>The entity instance, or <c>null</c> if not found.</returns>
    Task<TDto?> GetByIdAsync(Guid id);

    Task<IEnumerable<TDto>> GetAllAsync();

    Task<PaginatedResult<TDto>> GetPaginatedAsync(int page, int size);

    /// <summary>
    /// Creates and persists a new entity.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>The created entity.</returns>
    Task<TDto> CreateAsync(T entity);

    /// <summary>
    /// Updates and persists changes to an existing entity.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    /// <returns>The updated entity.</returns>
    Task<TDto> UpdateAsync(T entity);

    /// <summary>
    /// Performs a soft delete of the entity identified by the given ID.
    /// </summary>
    /// <param name="id">The ID of the entity to soft delete.</param>
    Task SoftDeleteAsync(Guid id);
}