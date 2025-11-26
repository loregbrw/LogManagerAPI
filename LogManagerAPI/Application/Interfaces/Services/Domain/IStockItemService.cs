namespace Application.Interfaces.Services.Domain;

using Application.Entities;
using Application.Enums;
using Application.Interfaces.Services.Domain.Primitives;
using Application.Models.Entities;
using Application.Models.Responses.Csv;
using Application.Models.Responses.StockItem;
using Application.Models.Responses.Value;

public interface IStockItemService : IBaseService<StockItem, StockItemDto>
{
    Task<GetValuesResponse> GetStockItemValuesAsync();
    Task<PaginatedStockItemResponse> GetPaginatedStockItemsAsync(
        int page,
        int size,
        string? search = null,
        EStockGroup? stockGroup = null,
        EStockItemStatus? status = null
    );

    Task<ImportCsvResponse> ImportFromCsvAsync(Stream fileStream);
    Task<ExportCsvResponse> ExportToCsvAsync(char? delimiter);
}