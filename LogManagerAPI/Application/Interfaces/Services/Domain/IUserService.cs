namespace Application.Interfaces.Services.Domain;

using Application.Entities;
using Application.Interfaces.Services.Domain.Primitives;
using Application.Models.Entities;
using Application.Models.Pagination;

public interface IUserService : IBaseService<User, UserDto>
{
    Task<PaginatedResult<UserDto>> GetPaginatedUsersAsync(int page, int size, string? search = null);
}