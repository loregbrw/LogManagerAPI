namespace Application.Interfaces.Services.Core.Auth;

using Application.Enums;
using Application.Models;

public interface IJwtService
{
    string GenerateToken(Guid userId, ERole userRole, TimeSpan? timeSpan = null);
    ContextData ValidateToken(string token);
    void ValidateTokenAndFillContext(string token);
}