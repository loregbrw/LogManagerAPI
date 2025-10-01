using Application.Interfaces.Services.Domain;
using Application.Models.Pagination;

namespace API.Features.User.Get;

public class GetPaginatedUsersHandler(IUserService service)
{
    private readonly IUserService _service = service;

}