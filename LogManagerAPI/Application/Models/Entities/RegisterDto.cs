namespace Application.Models.Entities;

using Application.Enums;
using Application.Models.Entities.Primitives;

public record RegisterDto(
    Guid Id,
    DateTime CreatedAt,
    ERegisterType RegisterType,
    StockItemDto StockItem,
    double Amount,
    UserDto? User,
    DateOnly Date,
    string? Observation
) : BaseDto(Id, CreatedAt);