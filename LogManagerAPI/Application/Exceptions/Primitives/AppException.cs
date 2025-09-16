
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

namespace Application.Exceptions.Primitives;

using System.Net;

/// <summary>
/// Represents a custom application exception with HTTP status code, title, and type URI, designed for error handling.
/// </summary>
public abstract class AppException : Exception
{
    /// <summary>
    /// Gets the HTTP status code associated with the exception.
    /// </summary>
    public int Status { get; }

    /// <summary>
    /// Gets the title of the error, typically the HTTP status name.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets the URI reference identifying the problem type.
    /// </summary>
    public string Type { get; }

    public string ResourceKey { get; }

    public object[]? Args { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppException"/> class
    /// with a specified error message, HTTP status code, and optional type URI.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="statusCode">The HTTP status code associated with the error.</param>
    /// <param name="type">An optional URI identifying the problem type.</param>
    public AppException(string resourceKey, HttpStatusCode statusCode, object[]? args = null, string? type = null)
        : base(resourceKey)
    {
        ResourceKey = resourceKey;
        Status = (int)statusCode;
        Title = statusCode.ToString();
        Args = args;
        Type = type ?? $"https://httpstatuses.com/{(int)statusCode}";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppException"/> class
    /// with a specified error message, HTTP status code, inner exception, and optional type URI.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="statusCode">The HTTP status code associated with the error.</param>
    /// <param name="inner">The inner exception that caused the current exception.</param>
    /// <param name="type">An optional URI identifying the problem type.</param>
    public AppException(string resourceKey, HttpStatusCode statusCode, Exception inner, object[]? args = null, string? type = null)
        : base(resourceKey, inner)
    {
        ResourceKey = resourceKey;
        Status = (int)statusCode;
        Title = statusCode.ToString();
        Args = args;
        Type = type ?? $"https://httpstatuses.com/{(int)statusCode}";
    }
}