namespace Application.Entities;

using Application.Entities.Primitives;

public class Client : BaseEntity
{
    public required string Name { get; set; }
}