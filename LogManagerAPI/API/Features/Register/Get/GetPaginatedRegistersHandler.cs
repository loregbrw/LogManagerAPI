namespace API.Features.Register.Get;

using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Pagination;

public class GetPaginatedRegistersHandler(IRegisterService service)
{
    private readonly IRegisterService _service = service;

    public async Task<PaginatedResult<RegisterDto>> HandleAsync(string? query, int? page, int? count)
    {
        var result = await _service.GetPaginatedRegistersAsync(page ?? 1, count ?? 10, query);

        return result;
    }
}
