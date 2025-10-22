namespace Application.Models.Pagination;

public record PaginationData(
    int Page,
    int PageSize,
    int TotalPages,
    int TotalItems,
    bool HasPreviousPage,
    bool HasNextPage
);