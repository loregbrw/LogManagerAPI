namespace Application.Models.Entities;

using Application.Models.Entities.Primitives;

public record StockSubgroupDto(
    Guid Id,
    DateTime CreatedAt,
    string Name
) : BaseDto(Id, CreatedAt);