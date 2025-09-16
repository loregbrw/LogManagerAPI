namespace Application.Entities.Primitives;

/// <summary>
/// Represents the base entity with common properties for all domain entities,
/// including a unique identifier and audit timestamps.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Whether entity is deleted.
    /// </summary>
    public bool IsDeleted => DeletedAt is not null;
}