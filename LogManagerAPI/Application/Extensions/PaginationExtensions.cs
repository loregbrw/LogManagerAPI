namespace Application.Extensions;

using Application.Models.Pagination;
using Microsoft.EntityFrameworkCore;

public static class PaginationExtensions
{
    public static PaginatedResult<T> ToPaginatedResult<T>(this IEnumerable<T> result, int page, int size)
    {
        if (page < 1) page = 1;
        if (size < 1) size = 10;

        var totalItems = result.Count();
        var totalPages = (int)Math.Ceiling(totalItems / (double)size);

        var items = result
            .Skip((page - 1) * size)
            .Take(size);

        return new PaginatedResult<T>
        (
            items,
            new PaginationData
            (
                page,
                size,
                totalPages,
                totalItems,
                page < totalPages,
                page > 1
            )
        );
    }

    // public static async Task<PaginatedResult<TDto>> ToPaginatedResultAsync<T, TDto>(this IQueryable<T> result, int page, int size)
    // {
    //     if (page < 1) page = 1;
    //     if (size < 1) size = 10;

    //     var totalItems = await result.CountAsync();
    //     var totalPages = (int)Math.Ceiling(totalItems / (double)size);

    //     var items = await result
    //         .Skip((page - 1) * size)
    //         .Take(size)
    //         .ToListAsync();

    //     return new PaginatedResult<TDto>
    //     (
    //         items,
    //         new PaginationData
    //         (
    //             page,
    //             size,
    //             totalPages,
    //             totalItems,
    //             page < totalPages,
    //             page > 1
    //         )
    //     );
    // }
}