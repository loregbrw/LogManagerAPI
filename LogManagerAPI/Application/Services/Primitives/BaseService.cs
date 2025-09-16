
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

namespace Application.Services.Primitives;

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
    public async Task<T> CreateAsync(T entity)
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