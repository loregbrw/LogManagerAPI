namespace API.Features.Auth;

using API.Attributes;
using Application.Interfaces.Services.Core;
using Application.Models.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost]
    [IgnoreAuthentication]
    public async Task<IActionResult> Login(
        [FromServices] IAuthService service, [FromBody] LoginPayload request
    )
    {
        var response = await service.LoginAsync(request);
        return Ok(response);
    }
}