namespace API.Features.StockDepartment;

using Application.Interfaces.Services.Domain;
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
        var result = await service.GetStockDepartmentValues();
        return Ok(result);
    }
}