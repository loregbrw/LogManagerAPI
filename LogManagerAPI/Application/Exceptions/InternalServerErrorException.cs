namespace Application.Exceptions;

using System.Net;
using Application.Exceptions.Primitives;

public sealed class InternalServerErrorException : AppException
{
    public InternalServerErrorException(string message, params object[] args)
        : base(message, HttpStatusCode.InternalServerError, args) { }

    public InternalServerErrorException(string message, Exception inner, params object[] args)
        : base(message, HttpStatusCode.InternalServerError, inner, args) { }
}