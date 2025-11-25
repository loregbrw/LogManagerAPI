namespace Application.Mappers;

using Application.Entities;
using Application.Interfaces.Mappers;
using Application.Models.Entities;

public class StockDepartmentMapper : IStockDepartmentMapper
{
    public StockDepartmentDto ToDto(StockDepartment entity)
    {
        return new StockDepartmentDto(
            entity.Id,
            entity.CreatedAt,
            entity.Name
        );
    }
}