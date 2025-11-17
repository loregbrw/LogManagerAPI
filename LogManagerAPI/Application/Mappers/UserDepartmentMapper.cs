namespace Application.Mappers;

using Application.Entities;
using Application.Interfaces.Mappers;
using Application.Models.Entities;

public class UserDepartmentMapper : IUserDepartmentMapper
{
    public UserDepartmentDto ToDto(UserDepartment entity)
    {
        return new UserDepartmentDto(
            entity.Id,
            entity.CreatedAt,
            entity.Name
        );
    }
}