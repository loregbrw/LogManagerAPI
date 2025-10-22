namespace Application.Mappers;

using Application.Entities;
using Application.Enums;
using Application.Mappers.Primitives;
using Application.Models.Entities;

public class StockItemMapper : IEntityMapper<StockItem, StockItemDto>
{
    public StockItemDto ToDto(StockItem entity)
    {
        return new StockItemDto(
            entity.Id,
            entity.CreatedAt,
            entity.Code,
            entity.Description,
            entity.StockGroup,
            entity.Current,
            entity.UnitOfMeasurement?.Name,
            entity.Current * entity.Cost,
            entity.MinimumStock,
            entity.Current switch
            {
                0 => EStockItemStatus.OUTOFSTOCK,
                _ when entity.Current < (entity.MinimumStock ?? 0) => EStockItemStatus.LOWSTOCK,
                _ => EStockItemStatus.INSTOCK
            }
        );
    }
}