namespace Application.Exceptions;

using System.Net;
using Application.Exceptions.Primitives;

public sealed class UnauthorizedException : AppException
{
    public UnauthorizedException(string message, params object[] args)
        : base(message, HttpStatusCode.Unauthorized, args) { }

    public UnauthorizedException(string message, Exception inner, params object[] args)
        : base(message, HttpStatusCode.Unauthorized, inner, args) { }
}