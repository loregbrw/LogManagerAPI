namespace Infrastructure.Services;

using System.Threading.Tasks;
using Application.Entities;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Core;
using Application.Interfaces.Services.Core.Auth;
using Application.Mappers.Primitives;
using Application.Models.Entities;
using Application.Models.Requests.Auth;
using Application.Models.Responses.Auth;

public class AuthService(IUserRepository repository, IPasswordHasher hasher, IJwtService jwtService, IEntityMapper<User, UserDto> mapper) : IAuthService
{
    private readonly IUserRepository _repo = repository;
    private readonly IPasswordHasher _hasher = hasher;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IEntityMapper<User, UserDto> _mapper = mapper;

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _repo.GetByEmailAsNoTrackingAsync(request.Email)
            ?? throw new NotFoundException("EntityNotFound", "Email");

        if (!_hasher.Verify(request.Password, user.Password))
            throw new UnauthorizedException("WrongPassword");

        var dto = _mapper.ToDto(user);
        var token = _jwtService.GenerateToken(dto);

        return new LoginResponse(
            token,
            dto
        );
    }
}