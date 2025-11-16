namespace Application.Mappers.Primitives;

using Application.Entities;
using Application.Models.Entities;
using Application.Models.Requests.User;

public interface IUserMapper : IEntityMapper<User, UserDto>
{
    User FromUserCsv(UserCsv entity);
    // UserCsv ToUserCsv(User entity);
}