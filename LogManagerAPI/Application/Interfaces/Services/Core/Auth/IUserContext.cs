namespace Application.Interfaces.Services.Core.Auth;

using Application.Enums;
using Application.Models;

public interface IUserContext
{
    Guid UserId { get; }
    ERole UserRole { get; }
    bool IsAuthenticated { get; }
    void Fill(ContextData data);
    void Clear();
}