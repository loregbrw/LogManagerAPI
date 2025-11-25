namespace Application.Mappers;

using Application.Entities;
using Application.Interfaces.Mappers;
using Application.Models.Entities;

public class StockSubgroupMapper : IStockSubgroupMapper
{
    public StockSubgroupDto ToDto(StockSubgroup entity)
    {
        return new StockSubgroupDto(
            entity.Id,
            entity.CreatedAt,
            entity.Name
        );
    }
}