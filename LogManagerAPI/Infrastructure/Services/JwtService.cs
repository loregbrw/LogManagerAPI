namespace Infrastructure.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Enums;
using Application.Exceptions;
using Application.Exceptions.Primitives;
using Application.Interfaces.Services.Core.Auth;
using Application.Models;
using Application.Models.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class JwtService : IJwtService
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private readonly IUserContext _userContext;
    private readonly JwtOptions _settings;
    private readonly SymmetricSecurityKey _securityKey;
    private readonly SigningCredentials _credentials;

    public JwtService(IUserContext userContext, IOptions<JwtOptions> options)
    {
        _userContext = userContext;
        _settings = options.Value;
        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
        _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
    }

    public string GenerateToken(Guid userId, ERole userRole, TimeSpan? timeSpan = null)
    {
        var claims = new List<Claim>
        {
            new("UserId", userId.ToString()),
            new("UserRole", userRole.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            claims: claims,
            expires: DateTime.UtcNow.Add(timeSpan ?? TimeSpan.FromMinutes(_settings.ExpirationInMinutes)),
            signingCredentials: _credentials
        );

        return _tokenHandler.WriteToken(token);
    }

    public ContextData ValidateToken(string token)
    {
        try
        {
            var claimsPrincipal = _tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _settings.Issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _securityKey,
                    ClockSkew = TimeSpan.Zero
                }, out _);

            var userId = claimsPrincipal.FindFirst("UserId")?.Value
                ?? throw new BadRequestException("MissingClaim", "UserId");

            var userRole = claimsPrincipal.FindFirst("UserRole")?.Value
                ?? throw new BadRequestException("MissingClaim", "UserRole");

            if (!Guid.TryParse(userId, out var parsedUserId))
                throw new BadRequestException("InvalidClaim", "UserId");

            if (!Enum.TryParse<ERole>(userRole, out var parsedUserRole))
                throw new BadRequestException("InvalidClaim", "UserRole");

            return new ContextData(parsedUserId, parsedUserRole);
        }
        catch (SecurityTokenArgumentException)
        {
            throw new UnauthorizedException("InvalidToken");
        }
        catch (SecurityTokenExpiredException)
        {
            throw new UnauthorizedException("TokenExpired");
        }
        catch (SecurityTokenException)
        {
            throw new UnauthorizedException("InvalidToken");
        }
        catch (Exception ex) when (ex is not AppException)
        {
            throw new InternalServerErrorException("TokenValidationFailed");
        }
    }

    public void ValidateTokenAndFillContext(string jwt)
    {
        var contextData = ValidateToken(jwt);
        _userContext.Fill(contextData);
    }
}