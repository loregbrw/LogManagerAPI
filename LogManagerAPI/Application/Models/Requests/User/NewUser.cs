namespace Application.Models.Requests.User;

using System.ComponentModel.DataAnnotations;
using Application.Enums;

public class NewUser
{
    public short? Code { get; set; }

    [StringLength(255)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Email { get; set; }

    [StringLength(255)]
    public string? Department { get; set; }

    [EnumDataType(typeof(ERole))]
    public ERole Role { get; set; } = ERole.DATA;
}