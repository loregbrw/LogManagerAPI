namespace Application.Services;

using System.Linq;
using System.Threading.Tasks;
using Application.Entities;
using Application.Extensions;
using Application.Interfaces.Repositories.Primitives;
using Application.Interfaces.Services.Domain;
using Application.Models.Pagination;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class UserService(
    IBaseRepository<User> repository
) : BaseService<User>(repository), IUserService
{
    public async Task RegisterNewUser()
    {

    }

    public async Task<PaginatedResult<User>> GetPaginatedUsers(int page, int size, string? search = null)
    {
        var query = _repo.GetAllAsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(u => EF.Functions.Like(u.Name, $"%{search}%"));

        return query.OrderBy(u => u.Name).ToPaginatedResult(page, size);
    }

}