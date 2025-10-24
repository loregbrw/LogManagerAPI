namespace Application.Entities;

using Application.Entities.Primitives;
using Application.Enums;

public class User : BaseEntity
{
    public required short Code { get; set; }
    public required string Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public ERole Role { get; set; } = ERole.DATA;
    public UserDepartment? UserDepartment { get; set; }
    public Image? ProfileImage { get; set; }
}