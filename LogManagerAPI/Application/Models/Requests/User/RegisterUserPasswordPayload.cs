namespace Application.Models.Requests.User;

using System.ComponentModel.DataAnnotations;

public class RegisterUserPasswordPayload
{
    [StringLength(50)]
    public required string UserPassword { get; set; }
}