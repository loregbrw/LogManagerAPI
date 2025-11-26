namespace Application.Services;

using System.Threading.Tasks;
using Application.Entities;
using Application.Exceptions;
using Application.Interfaces.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Requests.StockSubgroup;
using Application.Models.Responses.Value;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class StockSubgroupService(
    IStockSubgroupRepository repository, IStockSubgroupMapper mapper
) : BaseService<StockSubgroup, StockSubgroupDto>(repository, mapper), IStockSubgroupService
{
    private readonly IStockSubgroupRepository _repo = repository;
    private readonly IStockSubgroupMapper _mapper = mapper;

    public async Task<GetValuesResponse> GetStockSubgroupValuesAsync()
    {
        var values = await _repo.GetAllAsNoTracking()
            .OrderBy(s => s.Name)
            .Select(s => new ValueResponse(s.Id.ToString(), s.Name))
            .ToListAsync();

        return new GetValuesResponse(values);
    }

    public async Task<StockSubgroupDto> CreateStockSubGroupAsync(CreateStockSubgroupPayload payload)
    {
        var exists = await _repo.GetAllAsNoTracking()
            .AnyAsync(s => EF.Functions.ILike(s.Name, payload.Name));

        if (exists) throw new ConflictException("AlreadyExists", payload.Name);

        var subgroup = new StockSubgroup()
        {
            Name = payload.Name
        };

        await _repo.AddAsync(subgroup);
        await _repo.SaveChangesAsync();

        return _mapper.ToDto(subgroup);
    }
}