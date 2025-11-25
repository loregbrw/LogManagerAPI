namespace Application.Interfaces.Services.Domain;

using Application.Entities;
using Application.Interfaces.Services.Domain.Primitives;
using Application.Models.Entities;
using Application.Models.Responses.Value;

public interface IUserDepartmentService : IBaseService<UserDepartment, UserDepartmentDto>
{
    Task<GetValuesResponse> GetUserDepartmentValues();
}