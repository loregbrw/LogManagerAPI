namespace Application.Services;

using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Responses.Enum;
using Application.Models.Responses.Value;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class StockDepartmentService(
    IStockDepartmentRepository repository, IStockDepartmentMapper mapper
) : BaseService<StockDepartment, StockDepartmentDto>(repository, mapper), IStockDepartmentService
{

    private readonly IStockDepartmentRepository _repo = repository;

    public async Task<GetValuesResponse> GetStockDepartmentValues()
    {
        var values = await _repo.GetAllAsNoTracking()
            .OrderBy(d => d.Name)
            .Select(d => new ValueResponse(d.Id.ToString(), d.Name))
            .ToListAsync();

        return new GetValuesResponse(values);
    }
}