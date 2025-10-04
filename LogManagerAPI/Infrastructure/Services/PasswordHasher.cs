namespace Infrastructure.Services;

using Application.Interfaces.Services.Core.Auth;
using BCrypt.Net;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password) => BCrypt.HashPassword(password);
    public bool Verify(string password, string hashedPassword) => BCrypt.Verify(password, hashedPassword);
}