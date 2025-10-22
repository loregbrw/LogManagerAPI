namespace Application.Models.Entities;

using Application.Enums;
using Application.Models.Entities.Primitives;

public record StockItemDto(
    Guid Id,
    DateTime CreatedAt,
    string Code,
    string? Description,
    EStockGroup? StockGroup,
    long CurrentStock,
    string? UnitOfMeasurement,
    decimal? StockValue,
    short? MinimumStock,
    EStockItemStatus Status
) : BaseDto(Id, CreatedAt);