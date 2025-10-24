namespace API.Features.User.Get;

using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Pagination;

public class GetPaginatedUsersHandler(IUserService service)
{
    private readonly IUserService _service = service;

    public async Task<PaginatedResult<UserDto>> HandleAsync(string? query, int? page, int? count)
    {
        var result = await _service.GetPaginatedUsersAsync(page ?? 1, count ?? 10, query);

        return result;
    }
}
