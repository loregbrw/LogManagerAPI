namespace Application.Entities;

using Application.Entities.Primitives;
using Application.Enums;

public class User : BaseEntity
{
    public required short Code { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public ERole Role { get; set; } = ERole.USER;
    public Image? ProfileImage { get; set; }
}