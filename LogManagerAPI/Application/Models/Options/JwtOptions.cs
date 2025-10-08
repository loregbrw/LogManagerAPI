namespace Application.Models.Options;

public class JwtOptions
{
    public required string SecretKey { get; init; }
    public required string Issuer { get; init; }
    public required double ExpirationInMinutes { get; init;  }
}