namespace Application.Models.Entities;

using Application.Models.Entities.Primitives;

public record UserDepartmentDto(
    Guid Id,
    DateTime CreatedAt,
    string Name
) : BaseDto(Id, CreatedAt);