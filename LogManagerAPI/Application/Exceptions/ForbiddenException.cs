namespace Application.Exceptions;

using System.Net;
using Application.Exceptions.Primitives;

public sealed class ForbiddenException : AppException
{
    public ForbiddenException(string message, params object[] args)
        : base(message, HttpStatusCode.Forbidden, args) { }

    public ForbiddenException(string message, Exception inner, params object[] args)
        : base(message, HttpStatusCode.Forbidden, inner, args) { }
}