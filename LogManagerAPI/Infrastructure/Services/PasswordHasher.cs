namespace Infrastructure.Services;

using Application.Interfaces.Services.Core;
using BCrypt.Net;

/// <summary>
/// Implements <see cref="IPasswordHasher"/> using the BCrypt algorithm for password hashing and verification.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    /// <inheritdoc/>
    public string Hash(string password) => BCrypt.HashPassword(password);

    /// <inheritdoc/>
    public bool Verify(string password, string hashedPassword) => BCrypt.Verify(password, hashedPassword);
}