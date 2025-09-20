namespace Application.Models.Requests.User;

using System.ComponentModel.DataAnnotations;
using Application.Enums;

public class RegisterNewUserPayload
{
    [StringLength(255)]
    [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$", ErrorMessage = "O nome deve conter apenas letras.")]
    public string? Name { get; set; }


    [EmailAddress]
    [StringLength(255)]
    public required string Email { get; set; }

    [EnumDataType(typeof(ERole))]
    public required ERole Role { get; set; }
}