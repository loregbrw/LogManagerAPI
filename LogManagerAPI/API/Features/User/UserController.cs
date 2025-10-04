namespace API.Features.User;

using API.Attributes;
using API.Features.User.Get;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    [ManagerAuthentication]
    [HttpGet]
    public async Task<IActionResult> GetPaginatedUsers(
        [FromQuery] string? query, [FromQuery] int? page, [FromQuery] int? count
    )
    {
        var response = await handler.HandleAsync(query, page, count);
        // return Ok(response);

        throw new NotFoundException("EntityNotFound", ["User"]);
    }

    // [Http]
    // public asy
}