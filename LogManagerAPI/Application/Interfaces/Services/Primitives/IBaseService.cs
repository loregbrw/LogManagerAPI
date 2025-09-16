namespace Application.Interfaces.Services.Primitives;

using Application.Entities.Primitives;

/// <summary>
/// Defines basic CRUD operations for services working with entities of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The entity type, which must inherit from <see cref="BaseEntity"/>.</typeparam>
public interface IBaseService<T> where T : BaseEntity
{
    /// <summary>
    /// Retrieves an entity by its ID, or <c>null</c> if not found.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>The entity instance, or <c>null</c> if not found.</returns>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Creates and persists a new entity.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>The created entity.</returns>
    Task<T> CreateAsync(T entity);

    /// <summary>
    /// Updates and persists changes to an existing entity.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    /// <returns>The updated entity.</returns>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Performs a soft delete of the entity identified by the given ID.
    /// </summary>
    /// <param name="id">The ID of the entity to soft delete.</param>
    Task SoftDeleteAsync(Guid id);
}