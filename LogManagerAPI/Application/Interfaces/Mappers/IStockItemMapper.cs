namespace Application.Interfaces.Mappers;

using Application.Mappers.Primitives;
using Application.Entities;
using Application.Models.Entities;
using Application.Models.Requests.StockItem;

public interface IStockItemMapper : IEntityMapper<StockItem, StockItemDto>
{
    StockItem FromStockItemCsv(StockItemCsv entity);
    StockItemCsv ToStockItemCsv(StockItem entity);
}