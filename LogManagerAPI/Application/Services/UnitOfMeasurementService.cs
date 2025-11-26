namespace Application.Services;

using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces.Mappers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services.Domain;
using Application.Models.Entities;
using Application.Models.Responses.Value;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class UnitOfMeasurementService(
    IUnitOfMeasurementRepository repository, IUnitOfMeasurementMapper mapper
) : BaseService<UnitOfMeasurement, UnitOfMeasurementDto>(repository, mapper), IUnitOfMeasurementService
{

    private readonly IUnitOfMeasurementRepository _repo = repository;

    public async Task<GetValuesResponse> GetUnitOfMeasurementValuesAsync()
    {
        var values = await _repo.GetAllAsNoTracking()
            .OrderBy(u => u.Name)
            .Select(u => new ValueResponse(u.Id.ToString(), u.Name))
            .ToListAsync();

        return new GetValuesResponse(values);
    }
}