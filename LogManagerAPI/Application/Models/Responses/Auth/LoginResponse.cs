namespace Application.Models.Responses.Auth;

using Application.Models.Entities;

public record LoginResponse(
    string Token,
    UserDto User
);