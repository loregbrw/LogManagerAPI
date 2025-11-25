namespace Application.Interfaces.Services.Domain;

using Application.Entities;
using Application.Interfaces.Services.Domain.Primitives;
using Application.Models.Entities;
using Application.Models.Responses.Enum;

public interface IStockSubgroupService : IBaseService<StockSubgroup, StockSubgroupDto>
{
    Task<GetValuesResponse> GetStockSubgroupValues();
}