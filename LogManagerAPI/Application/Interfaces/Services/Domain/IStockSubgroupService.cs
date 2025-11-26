namespace Application.Interfaces.Services.Domain;

using Application.Entities;
using Application.Interfaces.Services.Domain.Primitives;
using Application.Models.Entities;
using Application.Models.Requests.StockSubgroup;
using Application.Models.Responses.Value;

public interface IStockSubgroupService : IBaseService<StockSubgroup, StockSubgroupDto>
{
    Task<GetValuesResponse> GetStockSubgroupValuesAsync();
    Task<StockSubgroupDto> CreateStockSubGroupAsync(CreateStockSubgroupPayload payload);
}