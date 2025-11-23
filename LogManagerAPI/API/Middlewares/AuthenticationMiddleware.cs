namespace API.Middlewares;

using System;
using API.Attributes;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces.Services.Core.Auth;
using Microsoft.AspNetCore.Http;

public class AuthenticationMiddleware(IJwtService jwtService, IUserContext userContext) : IMiddleware
{
    private readonly IJwtService _jwtService = jwtService;
    private readonly IUserContext _userContext = userContext;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var endpoint = context.GetEndpoint();

        if (ShouldSkipAuthentication(endpoint))
        {
            await next(context);
            return;
        }

        var auth = context.Request.Headers.Authorization.FirstOrDefault();

        if (!TryGetBearerToken(auth, out var token))
            throw new BadRequestException("MissingAuthorizationHeader");

        _jwtService.ValidateTokenAndFillContext(token!);

        if (!UserCanAccess(endpoint, _userContext))
            throw new ForbiddenException("ForbiddenEndpoint");

        await next(context);
    }

    private static bool ShouldSkipAuthentication(Endpoint? endpoint)
        => endpoint?.Metadata.GetMetadata<IgnoreAuthenticationAttribute>() is not null;

    private static bool UserCanAccess(Endpoint? endpoint, IUserContext context)
    {
        if (endpoint is null) return false;

        if (context.UserRole == ERole.DATA) return false;
        if (context.UserRole == ERole.ADMIN) return true;

        if (endpoint.Metadata.GetMetadata<AdminAuthenticationAttribute>() is not null)
            return false;

        if (endpoint.Metadata.GetMetadata<ManagerAuthenticationAttribute>() is not null)
            return context.UserRole == ERole.MANAGER;

        return true;
    }


    private static bool TryGetBearerToken(string? authHeader, out string? token)
    {
        token = null;

        if (string.IsNullOrWhiteSpace(authHeader))
            return false;

        var parts = authHeader.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
            return false;

        if (!parts[0].Equals("Bearer", StringComparison.OrdinalIgnoreCase))
            return false;

        token = parts[1];
        return !string.IsNullOrWhiteSpace(token);
    }
}
