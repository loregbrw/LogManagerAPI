namespace Application.Services;

using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Responses.Enum;
using Application.Models.Responses.Value;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class UserDepartmentService(
    IUserDepartmentRepository repository, IUserDepartmentMapper mapper
) : BaseService<UserDepartment, UserDepartmentDto>(repository, mapper), IUserDepartmentService
{

    private readonly IUserDepartmentRepository _repo = repository;

    public async Task<GetValuesResponse> GetUserDepartmentValues()
    {
        var values = await _repo.GetAllAsNoTracking()
            .OrderBy(d => d.Name)
            .Select(d => new ValueResponse(d.Id.ToString(), d.Name))
            .ToListAsync();

        return new GetValuesResponse(values);
    }
}