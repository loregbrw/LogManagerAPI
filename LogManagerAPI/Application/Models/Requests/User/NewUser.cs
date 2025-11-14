namespace Application.Models.Requests.User;

using System.ComponentModel.DataAnnotations;
using Application.Attributes;
using Application.Enums;

public class NewUser
{
    public short? Code { get; set; }

    [AppStringLength(255)]
    public string? Name { get; set; }

    [AppStringLength(255)]
    public string? Email { get; set; }

    [AppStringLength(255)]
    public string? Department { get; set; }

    [EnumDataType(typeof(ERole))]
    public ERole Role { get; set; } = ERole.DATA;
}