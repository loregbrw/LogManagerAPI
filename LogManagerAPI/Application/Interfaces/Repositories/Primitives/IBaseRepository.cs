
/*
    LogManager API
 - Inventory Management Software with incoming and outgoing stock control.
    Copyright (C) 2025 Lorena Gobara Falci

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    Contact: loregobara@gmail.com
*/

namespace Application.Interfaces.Repositories.Primitives;

using Application.Entities.Primitives;

/// <summary>
/// Defines a generic repository interface for entities inheriting from <see cref="BaseEntity"/>.
/// Supports basic CRUD operations with audit timestamps and soft delete handling.
/// </summary>
/// <typeparam name="T">The type of the entity, which must inherit from <see cref="BaseEntity"/>.</typeparam>
public interface IBaseRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Gets all entities with the default tracking behavior.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> for all entities.</returns>
    IQueryable<T> GetAll();

    /// <summary>
    /// Gets all entities as no-tracking (read-only).
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> for all entities without tracking.</returns>
    IQueryable<T> GetAllAsNoTracking();

    /// <summary>
    /// Gets all entities including those soft-deleted.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> including soft-deleted entities.</returns>
    IQueryable<T> GetAllIncludingDeleted();

    /// <summary>
    /// Gets all entities including soft-deleted ones, as no-tracking.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> including soft-deleted entities without tracking.</returns>
    IQueryable<T> GetAllIncludingDeletedAsNoTracking();

    /// <summary>
    /// Finds an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds an entity by its ID asynchronously without tracking.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<T?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new entity asynchronously and sets its creation timestamp.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous add operation.</returns>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds multiple new entities asynchronously and sets their creation timestamps.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous add operation.</returns>
    Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an entity and sets its update timestamp.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(T entity);

    /// <summary>
    /// Updates multiple entities and sets their update timestamps.
    /// </summary>
    /// <param name="entities">The entities to update.</param>
    void Update(IEnumerable<T> entities);

    /// <summary>
    /// Soft deletes an entity by setting its deletion timestamp.
    /// </summary>
    /// <param name="entity">The entity to soft delete.</param>
    public void SoftDelete(T entity);

    /// <summary>
    /// Soft deletes multiple entities by setting their deletion timestamps.
    /// </summary>
    /// <param name="entities">The entities to soft delete.</param>
    public void SoftDelete(IEnumerable<T> entities);

    /// <summary>
    /// Saves changes asynchronously to the database context.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}