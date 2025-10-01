namespace Application.Models.Entities.Primitives;

/// <summary>
/// Represents the base Data Transfer Object (DTO) for all entities,
/// containing common properties such as unique identifier and creation timestamp.
/// </summary>
/// <param name="Id">The unique identifier of the entity.</param>
/// <param name="CreatedAt">The date and time when the entity was created.</param>
public record BaseDto(
    Guid Id,
    DateTime CreatedAt
);
