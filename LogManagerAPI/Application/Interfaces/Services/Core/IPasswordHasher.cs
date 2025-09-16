namespace Application.Interfaces.Services.Core;

/// <summary>
/// Defines methods for hashing and verifying passwords.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Generates a hashed representation of the provided plaintext password.
    /// </summary>
    /// <param name="password">The plaintext password to hash.</param>
    /// <returns>The hashed password.</returns>
    string Hash(string password);

    /// <summary>
    /// Verifies whether the provided plaintext password matches the hashed password.
    /// </summary>
    /// <param name="password">The plaintext password to check.</param>
    /// <param name="hashedPassword">The previously hashed password to compare against.</param>
    /// <returns><c>true</c> if the password matches; otherwise, <c>false</c>.</returns>
    bool Verify(string password, string hashedPassword);
}