namespace Application.Mappers;

using Application.Entities;
using Application.Exceptions;
using Application.Interfaces.Mappers;
using Application.Models.Entities;
using Application.Models.Requests.User;

public class UserMapper : IUserMapper
{
    public UserDto ToDto(User entity)
    {
        return new UserDto(
            entity.Id,
            entity.CreatedAt,
            entity.Code,
            entity.Name,
            entity.UserDepartment?.Name,
            entity.Email,
            entity.Role,
            entity.ProfileImage?.FileGuid
        );
    }

    public User FromUserCsv(UserCsv entity)
    {
        return new User
        {
            Code = entity.Code ?? throw new InternalServerErrorException("UnknownErrorMapping"),
            Name = entity.Name ?? throw new InternalServerErrorException("UnknownErrorMapping"),
            Email = entity.Email,
            Role = entity.Role
        };
    }

    public UserCsv ToUserCsv(User entity)
    {
        return new UserCsv
        {
            Code = entity.Code,
            Name = entity.Name,
            Email = entity.Email,
            Department = entity.UserDepartment?.Name,
            Role = entity.Role
        };
    }
}
