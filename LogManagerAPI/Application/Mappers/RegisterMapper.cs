namespace Application.Mappers;

using Application.Entities;
using Application.Interfaces.Mappers;
using Application.Models.Entities;

public class RegisterMapper(IStockItemMapper stockItemMapper, IUserMapper userMapper) : IRegisterMapper
{
    private readonly IStockItemMapper _stockItemMapper = stockItemMapper;
    private readonly IUserMapper _userMapper = userMapper;

    public RegisterDto ToDto(Register entity)
    {
        return new RegisterDto(
            entity.Id,
            entity.CreatedAt,
            entity.RegisterType,
            _stockItemMapper.ToDto(entity.StockItem),
            entity.Amount,
            entity.User is not null ? _userMapper.ToDto(entity.User) : null,
            entity.Date,
            entity.Observation
        );
    }
}