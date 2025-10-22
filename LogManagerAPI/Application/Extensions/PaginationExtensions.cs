namespace Application.Extensions;

using Application.Mappers.Primitives;
using Application.Models.Pagination;
using Microsoft.EntityFrameworkCore;

public static class PaginationExtensions
{

    public static PaginatedResult<TDto> ToPaginatedResult<TDto>(this IEnumerable<TDto> result, int page, int size)
    {
        if (page < 1) page = 1;
        if (size < 1) size = 10;

        var totalItems = result.Count();
        var totalPages = (int)Math.Ceiling(totalItems / (double)size);

        var items = result
            .Skip((page - 1) * size)
            .Take(size);

        return new PaginatedResult<TDto>
        (
            items,
            new PaginationData
            (
                page,
                size,
                totalPages,
                totalItems,
                page > 1,
                page < totalPages
            )
        );
    }

    public static async Task<PaginatedResult<TDto>> ToPaginatedResultAsync<T, TDto>(this IQueryable<T> result, IEntityMapper<T, TDto> mapper, int page, int size)
    {
        if (page < 1) page = 1;
        if (size < 1) size = 10;

        var totalItems = await result.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)size);

        var items = await result
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        return new PaginatedResult<TDto>
        (
            items.Select(mapper.ToDto),
            new PaginationData
            (
                page,
                size,
                totalPages,
                totalItems,
                page > 1,
                page < totalPages
            )
        );
    }
}