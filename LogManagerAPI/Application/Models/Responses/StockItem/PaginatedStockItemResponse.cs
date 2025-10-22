namespace Application.Models.Responses.StockItem;

using Application.Models.Entities;
using Application.Models.Pagination;

public record PaginatedStockItemResponse(
    StockResponse Overview,
    IEnumerable<StockItemDto> PaginatedItems,
    PaginationData Pagination
);