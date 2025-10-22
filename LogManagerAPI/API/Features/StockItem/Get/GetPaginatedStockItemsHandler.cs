namespace API.Features.StockItem.Get;

using Application.Interfaces.Services.Domain;
using Application.Enums;
using Application.Models.Responses.StockItem;

public class GetPaginatedStockItemsHandler(IStockItemService service)
{
    private readonly IStockItemService _service = service;

    public async Task<PaginatedStockItemResponse> HandleAsync(string? query, int? page, int? count, EStockGroup? stockGroup, EStockItemStatus? status)
    {
        var result = await _service.GetPaginatedStockItemsAsync(page ?? 1, count ?? 10, query, stockGroup, status);

        return result;
    }
}