namespace Application.Models.Requests.Auth;

using System.ComponentModel.DataAnnotations;

public class LoginPayload
{
    [Required]
    [StringLength(255)]
    public required string Email { get; init; }

    [Required]
    [StringLength(50)]
    public required string Password { get; init; }
}