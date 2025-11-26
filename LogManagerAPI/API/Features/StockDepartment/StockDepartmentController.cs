namespace API.Features.StockDepartment;

using Application.Interfaces.Services.Domain;
using Application.Models.Requests.StockDepartment;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stock-departments")]
public class StockDepartmentController : ControllerBase
{
    [HttpGet("values")]
    public async Task<IActionResult> GetStockDepartmentValues(
        [FromServices] IStockDepartmentService service
    )
    {
        var result = await service.GetStockDepartmentValuesAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubgroup(
    [FromServices] IStockDepartmentService service, [FromBody] CreateStockDepartmentPayload payload
)
    {
        var result = await service.CreateStockDepartmentAsync(payload);
        return Created("/api/stock-departments", result);
    }
}   