namespace Application.Interfaces.Services.Domain;

using Application.Entities;
using Application.Interfaces.Services.Domain.Primitives;
using Application.Models.Entities;
using Application.Models.Pagination;
using Application.Models.Requests.Register;

public interface IRegisterService : IBaseService<Register, RegisterDto>
{
    Task<RegisterDto> CreateRegisterAsync(CreateRegisterPayload payload, Guid userId);
    Task<PaginatedResult<RegisterDto>> GetPaginatedRegistersAsync(int page, int size, string? search = null);
}