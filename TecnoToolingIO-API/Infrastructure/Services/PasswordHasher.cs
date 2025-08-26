
/*
    TecnoToolingIO API - Inventory Management Software with incoming and outgoing stock control.
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

namespace Infrastructure.Services;

using Application.Interfaces.Services;
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