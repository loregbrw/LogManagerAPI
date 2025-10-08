namespace API.Features.User.Get;

using Application.Exceptions;
using Application.Interfaces.Services.Core.Auth;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;

public class GetLoggedUserHandler(IUserService service, IUserContext context)
{
    private readonly IUserService _service = service;
    private readonly IUserContext _context = context;

    public async Task<UserDto> HandleAsync()
    {
        var dto = await _service.GetByIdAsync(_context.UserId) ??
            throw new InternalServerErrorException("UnknownError");

        return dto;
    }
}