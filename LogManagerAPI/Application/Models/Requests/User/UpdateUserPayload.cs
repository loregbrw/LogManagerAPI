using System.ComponentModel.DataAnnotations;
using Application.Enums;

namespace Application.Models.Requests.User;

public class UpdateUserPayload
{
    public short? Code { get; set; }

    [StringLength(255)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? Password { get; set; }
    public Guid? UserDepartmentId { get; set; }
    
    [EnumDataType(typeof(ERole))]
    public ERole? UserRole { get; set; }
}