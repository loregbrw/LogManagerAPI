namespace Application.Models.Requests.User;

using System.ComponentModel.DataAnnotations;
using Application.Enums;

public class RegisterNewUserPayload
{
    public required Guid UserId { get; set; }
    
    [EnumDataType(typeof(ERole))]
    public ERole UserRole { get; set; } = ERole.USER;
}