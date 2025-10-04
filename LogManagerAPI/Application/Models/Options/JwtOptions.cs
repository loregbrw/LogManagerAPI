namespace Application.Models.Options;

public class JwtOptions
{
    public required string SecretKey { get; init; }
    public required string Issuer { get; init; }
}