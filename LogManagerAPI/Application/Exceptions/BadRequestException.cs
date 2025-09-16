namespace Application.Exceptions;

using System.Net;
using Application.Exceptions.Primitives;

public sealed class BadRequestException : AppException
{
    public BadRequestException(string message, params object[] args)
        : base(message, HttpStatusCode.BadRequest, args) { }

    public BadRequestException(string message, Exception inner, params object[] args)
        : base(message, HttpStatusCode.BadRequest, inner, args) { }
}