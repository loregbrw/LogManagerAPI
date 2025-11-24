namespace Application.Mappers;

using Application.Entities;
using Application.Interfaces.Mappers;
using Application.Models.Entities;

public class UnitOfMeasurementMapper : IUnitOfMeasurementMapper
{
    public UnitOfMeasurementDto ToDto(UnitOfMeasurement entity)
    {
        return new UnitOfMeasurementDto(
            entity.Id,
            entity.CreatedAt,
            entity.Name
        );
    }
}