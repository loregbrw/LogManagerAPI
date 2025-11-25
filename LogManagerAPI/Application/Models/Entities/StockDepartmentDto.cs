namespace Application.Models.Entities;

using Application.Models.Entities.Primitives;

public record StockDepartmentDto(
    Guid Id,
    DateTime CreatedAt,
    string Name
) : BaseDto(Id, CreatedAt);