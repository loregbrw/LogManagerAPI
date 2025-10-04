namespace API.Features.User.Get;

using System.Linq.Expressions;
using Application.Entities;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Pagination;
using Microsoft.EntityFrameworkCore;

public class GetPaginatedUsersHandler(IUserService service)
{
    private readonly IUserService _service = service;

    public async Task<PaginatedResult<UserDto>> HandleAsync(string? query, int? page, int? count)
    {
        Expression<Func<User, bool>>? filter = null;

        if (!string.IsNullOrWhiteSpace(query))
        {
            filter = u => EF.Functions.Like(u.Name, $"%{query}%");
        }

        var result = await _service.GetPaginatedAsync(page ?? 1, count ?? 10, filter);

        return result;
    }
}