namespace Application.Exceptions;

using System.Net;
using Application.Exceptions.Primitives;

public sealed class NotFoundException : AppException
{
    public NotFoundException(string message, params object[] args)
        : base(message, HttpStatusCode.NotFound, args) { }

    public NotFoundException(string message, Exception inner, params object[] args)
        : base(message, HttpStatusCode.NotFound, inner, args) { }
}