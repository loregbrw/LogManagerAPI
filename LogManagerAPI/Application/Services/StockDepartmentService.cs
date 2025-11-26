namespace Application.Services;

using System.Threading.Tasks;
using Application.Entities;
using Application.Exceptions;
using Application.Interfaces.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Requests.StockDepartment;
using Application.Models.Responses.Value;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class StockDepartmentService(
    IStockDepartmentRepository repository, IStockDepartmentMapper mapper
) : BaseService<StockDepartment, StockDepartmentDto>(repository, mapper), IStockDepartmentService
{

    private readonly IStockDepartmentRepository _repo = repository;
    private readonly IStockDepartmentMapper _mapper = mapper;

    public async Task<GetValuesResponse> GetStockDepartmentValuesAsync()
    {
        var values = await _repo.GetAllAsNoTracking()
            .OrderBy(d => d.Name)
            .Select(d => new ValueResponse(d.Id.ToString(), d.Name))
            .ToListAsync();

        return new GetValuesResponse(values);
    }

    public async Task<StockDepartmentDto> CreateStockDepartmentAsync(CreateStockDepartmentPayload payload)
    {
        var exists = await _repo.GetAllAsNoTracking()
            .AnyAsync(s => EF.Functions.ILike(s.Name, payload.Name));

        if (exists) throw new ConflictException("AlreadyExists", payload.Name);

        var department = new StockDepartment()
        {
            Name = payload.Name
        };

        await _repo.AddAsync(department);
        await _repo.SaveChangesAsync();

        return _mapper.ToDto(department);
    }
}