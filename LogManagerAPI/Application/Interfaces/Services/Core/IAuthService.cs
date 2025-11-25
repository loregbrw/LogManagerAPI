namespace Application.Interfaces.Services.Core;

using Application.Models.Requests.Auth;
using Application.Models.Responses.Auth;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginPayload request);
}