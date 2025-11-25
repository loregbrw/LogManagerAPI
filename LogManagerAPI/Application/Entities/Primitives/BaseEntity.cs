namespace Application.Entities.Primitives;

public abstract class BaseEntity
{
    public Guid Id { get; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted => DeletedAt is not null;
}