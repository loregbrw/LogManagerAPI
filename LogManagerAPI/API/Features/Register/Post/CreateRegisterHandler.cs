namespace API.Features.Register.Post;

using Application.Interfaces.Services.Core.Auth;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Requests.Register;

public class CreateRegisterHandler(IRegisterService service, IUserContext context)
{
    private readonly IRegisterService _service = service;
    private readonly IUserContext _context = context;

    public async Task<RegisterDto> HandleAsync(CreateRegisterPayload payload)
    {
        return await _service.CreateRegisterAsync(payload, _context.UserId);
    }
}