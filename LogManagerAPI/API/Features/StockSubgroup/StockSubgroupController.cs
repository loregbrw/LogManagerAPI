namespace API.Features.StockSubgroup;

using Application.Interfaces.Services.Domain;
using Application.Models.Requests.StockSubgroup;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stock-subgroups")]
public class StockSubgroupController : ControllerBase
{
    [HttpGet("values")]
    public async Task<IActionResult> GetStockSubgroupValues(
        [FromServices] IStockSubgroupService service
    )
    {
        var result = await service.GetStockSubgroupValues();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubgroup(
        [FromServices] IStockSubgroupService service, [FromBody] CreateStockSubgroupPayload payload
    )
    {
        var result = await service.CreateStockSubGroupAsync(payload);
        return Created("/api/stock-subgroups", result);
    }
}