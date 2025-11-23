namespace Application.Models.Options;

public class EmailSenderOptions
{
    public required string DisplayName { get; set; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}