namespace Application.Services;

using Application.Entities;
using Application.Exceptions;
using Application.Interfaces.Mappers;
using Application.Interfaces.Providers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Requests.Register;
using Application.Services.Primitives;

public class RegisterService(
    IRegisterRepository repository, IUserRepository userRepository, IStockItemRepository stockItemRepository,
    IRegisterMapper mapper, IDateTimeProvider dateTimeProvider
) : BaseService<Register, RegisterDto>(repository, mapper), IRegisterService
{
    private readonly IRegisterRepository _repo = repository;
    private readonly IRegisterMapper _mapper = mapper;
    private readonly IUserRepository  _userRepo = userRepository;
    private readonly IStockItemRepository _stockItemRepo = stockItemRepository;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<RegisterDto> CreateRegisterAsync(CreateRegisterPayload payload, Guid userId)
    {
        var user = await _userRepo.GetByIdAsync(userId) ?? throw new NotFoundException("EntityNotFound", typeof(User));
        var stockItem = await _stockItemRepo.GetByIdAsync(payload.StockItemId) ?? throw new NotFoundException("EntityNotFound", typeof(StockItem));

        var Register = new Register()
        {
            RegisterType = payload.RegisterType,
            StockItem = stockItem,
            Amount = payload.Amount,
            User = user,
            Date = DateOnly.FromDateTime(_dateTimeProvider.UtcNow),
            Observation = payload.Observation
        };

        await _repo.AddAsync(Register);
        await _repo.SaveChangesAsync();

        return _mapper.ToDto(Register);
    }
}