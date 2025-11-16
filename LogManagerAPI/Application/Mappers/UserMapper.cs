namespace Application.Mappers;

using Application.Entities;
using Application.Exceptions;
using Application.Mappers.Primitives;
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
}
