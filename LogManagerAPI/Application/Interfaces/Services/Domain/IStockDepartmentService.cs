namespace Application.Interfaces.Services.Domain;

using Application.Entities;
using Application.Interfaces.Services.Domain.Primitives;
using Application.Models.Entities;
using Application.Models.Requests.StockDepartment;
using Application.Models.Responses.Value;

public interface IStockDepartmentService : IBaseService<StockDepartment, StockDepartmentDto>
{
    Task<GetValuesResponse> GetStockDepartmentValues();
    Task<StockDepartmentDto> CreateStockDepartmentAsync(CreateStockDepartmentPayload payload);
}