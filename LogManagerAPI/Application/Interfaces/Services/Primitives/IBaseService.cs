
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