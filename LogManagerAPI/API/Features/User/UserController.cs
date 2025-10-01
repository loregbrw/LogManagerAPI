namespace API.Features.User;

using API.Attributes;
using API.Features.User.Get;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    [ManagerAuthentication]
    [HttpGet]
    public async Task<IActionResult> GetPaginatedUsers(
        [FromServices] GetPaginatedUsersHandler handler,
        [FromQuery] string? query, [FromQuery] int? page, [FromQuery] int? count
    )
    {
        var response = await handler.HandleAsync(query, page, count);
        return Ok(response);
    }

    // [HttpGet]
    // public async Task<IActionResult> GetUsers(
    //     [FromServices] IUserService service
    // )
    // {
    //     var aa = await service.
    // }
}