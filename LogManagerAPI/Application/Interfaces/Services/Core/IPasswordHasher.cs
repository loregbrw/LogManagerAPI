/*
    LogManager API
 - Inventory Management Software with incoming and outgoing stock control.
    Copyright (C) 2025 Lorena Gobara Falci

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    Contact: loregobara@gmail.com
*/

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