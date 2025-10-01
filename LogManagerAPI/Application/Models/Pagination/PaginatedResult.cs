namespace Application.Models.Pagination;

public record PaginatedResult<T>(
    IEnumerable<T> PaginatedItems,
    PaginationData Pagination
);