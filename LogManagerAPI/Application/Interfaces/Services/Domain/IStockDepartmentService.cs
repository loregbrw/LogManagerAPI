namespace Application.Interfaces.Services.Domain;

using Application.Entities;
using Application.Interfaces.Services.Domain.Primitives;
using Application.Models.Entities;
using Application.Models.Responses.Enum;

public interface IStockDepartmentService : IBaseService<StockDepartment, StockDepartmentDto>
{
    Task<GetValuesResponse> GetStockDepartmentValues();
}