namespace Infrastructure.Services;

using Application.Enums;
using Application.Exceptions;
using Application.Interfaces.Services.Core.Auth;
using Application.Models;

public class UserContext : IUserContext
{
    private ContextData? _data;

    public Guid UserId => _data?.UserId ?? throw new UnauthorizedException("NotAuthenticated");

    public ERole UserRole => _data?.UserRole ?? throw new UnauthorizedException("NotAuthenticated");

    public bool IsAuthenticated => _data.HasValue;

    public void Fill(ContextData data) => _data = data;

    public void Clear() => _data = null;
}