namespace Application.Models.Requests.Employee;

using System.ComponentModel.DataAnnotations;
using Application.Enums;

public class CreateEmployee
{
    public required short Code { get; set; }

    [StringLength(255)]
    public required string Name { get; set; }

    [StringLength(255)]
    public required string Email { get; set; }

    [StringLength(50)]
    public required string Password { get; set; }

    [EnumDataType(typeof(ERole))]
    public required ERole Role { get; set; }
}