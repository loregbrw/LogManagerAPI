namespace Application.Services;

using Application.Entities;
using Application.Enums;
using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces.Mappers;
using Application.Interfaces.Providers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Pagination;
using Application.Models.Requests.Register;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class RegisterService(
    IRegisterRepository repository, IUserRepository userRepository, IStockItemRepository stockItemRepository,
    IRegisterMapper mapper, IDateTimeProvider dateTimeProvider
) : BaseService<Register, RegisterDto>(repository, mapper), IRegisterService
{
    private readonly IRegisterRepository _repo = repository;
    private readonly IRegisterMapper _mapper = mapper;
    private readonly IUserRepository _userRepo = userRepository;
    private readonly IStockItemRepository _stockItemRepo = stockItemRepository;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<RegisterDto> CreateRegisterAsync(CreateRegisterPayload payload, Guid userId)
    {
        var user = await _userRepo.GetByIdAsync(userId) ?? throw new NotFoundException("EntityNotFound", typeof(User).Name);
        var stockItem = await _stockItemRepo.GetByIdAsync(payload.StockItemId) ?? throw new NotFoundException("EntityNotFound", typeof(StockItem).Name);

        switch (payload.RegisterType)
        {
            case ERegisterType.INBOUND:
                stockItem.Inbound += (long)payload.Amount;
                stockItem.Current += (long)payload.Amount;
                break;

            case ERegisterType.OUTBOUND:
                if (payload.Amount > stockItem.Current)
                    throw new BadRequestException("InsufficientBalance");

                stockItem.Outbound += (long)payload.Amount;
                stockItem.Current -= (long)payload.Amount;
                break;

            case ERegisterType.FIX:
                stockItem.Current = (long)payload.Amount;
                break;
        }

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

    public async Task<PaginatedResult<RegisterDto>> GetPaginatedRegistersAsync(int page, int size, string? search = null)
    {
        var query = _repo.GetAllAsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(r =>
                EF.Functions.ILike(r.StockItem.Code, $"%{search}%") ||
                EF.Functions.ILike(r.StockItem.Description!, $"%{search}%"));

        return await query
            .Include(r => r.StockItem)
            .OrderByDescending(r => r.Date)
            .ThenByDescending(r => r.CreatedAt)
            .ToPaginatedResultAsync(_mapper, page, size);
    }
}