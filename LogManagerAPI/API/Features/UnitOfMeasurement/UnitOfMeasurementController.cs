namespace API.Features.UnitOfMeasurement;

using Application.Interfaces.Services.Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/units-of-measurement")]
public class UnitOfMeasurementController : ControllerBase
{
    [HttpGet("values")]
    public async Task<IActionResult> GetUnitOfMeasurementValues(
        [FromServices] IUnitOfMeasurementService service
    )
    {
        var response = await service.GetUnitOfMeasurementValues();
        return Ok(response);
    }

}