namespace Application.Services;

using System.Linq;
using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces.Repositories.Primitives;
using Application.Interfaces.Services.Domain;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class UserService(
    IBaseRepository<User> repository
) : BaseService<User>(repository), IUserService
{
    public async Task RegisterNewUser()
    {

    }

    public async Task GetPaginatedUsers(int page, int size)
    {
        int skip = (page - 1) * size;

        var Users = await _repo.GetAllAsNoTracking()
            .OrderBy(e => e.Name)
            .Skip(skip)
            .Take(size)
            .ToListAsync();
    }

}