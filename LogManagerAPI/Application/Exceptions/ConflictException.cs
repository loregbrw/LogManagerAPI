namespace Application.Exceptions;

using System.Net;
using Application.Exceptions.Primitives;

public sealed class ConflictException : AppException
{
    public ConflictException(string message, params object[] args)
        : base(message, HttpStatusCode.Conflict, args) { }

    public ConflictException(string message, Exception inner, params object[] args)
        : base(message, HttpStatusCode.Conflict, inner, args) { }
}