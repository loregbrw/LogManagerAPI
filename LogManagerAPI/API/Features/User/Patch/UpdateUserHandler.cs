namespace API.Features.User.Patch;

using Application.Interfaces.Services.Core.Auth;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Requests.User;

public class UpdateUserHandler(IUserService service, IUserContext context)
{
    private readonly IUserService _service = service;
    private readonly IUserContext _context = context;

    public async Task<UserDto> HandleAsync(Guid? id, UpdateUserPayload payload)
    {
        return await _service.UpdateUserAsync(id ?? _context.UserId, payload);
    }

}