namespace API.Features.Register;

using API.Features.Register.Post;
using Application.Models.Requests.Register;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/registers")]
public class RegisterController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateRegister(
        [FromServices] CreateRegisterHandler handler, [FromBody] CreateRegisterPayload payload
    )
    {
        var result = await handler.HandleAsync(payload);
        return Ok(result);
    }
}