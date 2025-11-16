namespace Application.Mappers;

using Application.Entities;
using Application.Enums;
using Application.Exceptions;
using Application.Mappers.Primitives;
using Application.Models.Entities;
using Application.Models.Requests.StockItem;

public class StockItemMapper : IStockItemMapper
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

    public StockItem FromNewStockItem(NewStockItem entity)
    {
        return new StockItem
        {
            Code = entity.Code ?? throw new InternalServerErrorException("UnknownErrorMapping"),
            Description = entity.Description,
            Localization = entity.Localization,
            StockGroup = entity.Group,
            Cost = entity.Cost,
            MinimumStock = entity.MinimumStock,
            Inbound = entity.Inbound,
            Outbound = entity.Outbound,
            Current = entity.Current,
            StockSituation = entity.Situation
        };
    }
}