namespace Application.Mappers;

using Application.Entities;
using Application.Mappers.Primitives;
using Application.Models.Entities;

public class UserMapper : IEntityMapper<User, UserDto>
{
    public UserDto ToDto(User entity)
    {
        return new UserDto(
            entity.Id,
            entity.CreatedAt,
            entity.Code,
            entity.Name,
            entity.Email,
            entity.Role,
            entity.ProfileImage?.FileGuid
        );
    }
}
