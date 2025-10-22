namespace Application.Services;

using System.Linq;
using System.Threading.Tasks;
using Application.Entities;
using Application.Extensions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Domain;
using Application.Mappers.Primitives;
using Application.Models.Entities;
using Application.Models.Pagination;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class UserService(
    IUserRepository repository,
    IEntityMapper<User, UserDto> mapper
) : BaseService<User, UserDto>(repository, mapper), IUserService
{
    // public async Task RegisterNewUser()
    // {

    // }

    public async Task<PaginatedResult<UserDto>> GetPaginatedUsersAsync(int page, int size, string? search = null)
    {
        var query = _repo.GetAllAsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(u => EF.Functions.Like(u.Name, $"%{search}%"));

        return await query.OrderBy(u => u.Name).ToPaginatedResultAsync(_mapper, page, size);
    }

}