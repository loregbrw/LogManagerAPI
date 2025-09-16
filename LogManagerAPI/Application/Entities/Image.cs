namespace Application.Entities;

using Application.Entities.Primitives;

public class Image : BaseEntity
{
    public required Guid FileGuid { get; init; }
    public required byte[] ImageContentS { get; init; }
    public required byte[] ImageContentM { get; init; }
    public required byte[] ImageContentL { get; init; }
    public required string Extension { get; init; }
}