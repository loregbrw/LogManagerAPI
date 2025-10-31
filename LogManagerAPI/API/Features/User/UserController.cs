namespace API.Features.User;

using API.Attributes;
using API.Features.User.Get;
using API.Features.User.Post;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    [HttpGet("paginated")]
    [ManagerAuthentication]
    public async Task<IActionResult> GetPaginatedUsers(
        [FromServices] GetPaginatedUsersHandler handler,
        [FromQuery] string? query, [FromQuery] int? page, [FromQuery] int? count
    )
    {
        var response = await handler.HandleAsync(query, page, count);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetLoggedUser(
        [FromServices] GetLoggedUserHandler handler
    )
    {
        var response = await handler.HandleAsync();
        return Ok(response);
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportUsers(
        [FromServices] ImportUsersHandler handler, [FromForm] IFormFile file
    )
    {
        var response = await handler.HandleAsync(file);
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