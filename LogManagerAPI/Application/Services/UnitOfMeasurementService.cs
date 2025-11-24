namespace Application.Services;

using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Responses.Enum;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class UnitOfMeasurementService(
    IUnitOfMeasurementRepository repository, IUnitOfMeasurementMapper mapper
) : BaseService<UnitOfMeasurement, UnitOfMeasurementDto>(repository, mapper), IUnitOfMeasurementService
{

    private readonly IUnitOfMeasurementRepository _repo = repository;

    public async Task<GetValuesResponse> GetUnitOfMeasurementValues()
    {
        var values = await _repo.GetAllAsNoTracking()
            .OrderBy(d => d.Name)
            .Select(d => new ValueResponse(d.Id.ToString(), d.Name))
            .ToListAsync();

        return new GetValuesResponse(values);
    }
}