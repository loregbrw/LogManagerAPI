namespace API.Features.StockSubgroup;

using Application.Interfaces.Services.Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/stock-subgroups")]
public class StockSubgroupController : ControllerBase
{
    [HttpGet("values")]
    public async Task<IActionResult> StockSubgroups(
        [FromServices] IStockSubgroupService service
    )
    {
        var response = await service.GetStockSubgroupValues();
        return Ok(response);
    }
}