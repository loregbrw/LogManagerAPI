namespace Application.Interfaces.Services.Core;

using Application.Models.Entities;

public interface IJwtService
{
    string GenerateToken(UserDto user);
    void ValidateToken(string token);
}