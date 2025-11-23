namespace Application.Models.Requests.User;

using System.ComponentModel.DataAnnotations;

public class CreateUserPayload
{
    public required short Code { get; set; }

    [StringLength(255)]
    public required string Name { get; set; }

    [StringLength(255)]
    public string? Email { get; set; }

    public Guid? UserDepartmentId { get; set; }
}