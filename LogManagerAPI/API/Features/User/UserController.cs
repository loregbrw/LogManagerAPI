namespace API.Features.User;

using API.Attributes;
using API.Features.User.Get;
using API.Features.User.Patch;
using API.Features.User.Post;
using Application.Interfaces.Services.Domain;
using Application.Models.Requests.User;
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
        var result = await handler.HandleAsync(query, page, count);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetLoggedUser(
        [FromServices] GetLoggedUserHandler handler
    )
    {
        var result = await handler.HandleAsync();
        return Ok(result);
    }

    [HttpPost]
    [ManagerAuthentication]
    public async Task<IActionResult> CreateUser(
        [FromServices] IUserService service, [FromBody] CreateUserPayload payload
    )
    {
        var result = await service.CreateUserAsync(payload);
        return Created("/api/users", result);
    }

    [HttpPost("import")]
    [ManagerAuthentication]
    public async Task<IActionResult> ImportUsers(
        [FromServices] ImportUsersHandler handler, [FromForm] IFormFile file
    )
    {
        var result = await handler.HandleAsync(file);
        return Ok(result);
    }

    [HttpPost("register")]
    [ManagerAuthentication]
    public async Task<IActionResult> RegisterNewUser(
        [FromServices] IUserService service, [FromBody] RegisterNewUserPayload payload
    )
    {
        await service.RegisterUserAsync(payload);
        return Created();
    }

    [HttpGet("register")]
    [IgnoreAuthentication]
    public async Task<IActionResult> GetRegisteringUser(
        [FromServices] IUserService service, [FromQuery] string token
    )
    {
        var result = await service.GetRegisteringUserAsync(token);
        return Ok(result);
    }

    [HttpPost("complete-register")]
    [IgnoreAuthentication]
    public async Task<IActionResult> CompleteUserRegistration(
    [FromServices] IUserService service, [FromBody] RegisterUserPasswordPayload payload, [FromQuery] string token
    )
    {
        var result = await service.CompleteUserRegistrationAsync(token, payload);
        return Ok(result);
    }

    [HttpGet("export")]
    [ManagerAuthentication]
    public async Task<IActionResult> ExportUsers(
        [FromServices] IUserService service, [FromQuery] char? delimiter
    )
    {
        var result = await service.ExportToCsvAsync(delimiter);
        return File(result.Content, result.ContentType, result.FileName);
    }

    [HttpGet("roles/values")]
    [ManagerAuthentication]
    public IActionResult GetUserRoles(
        [FromServices] IUserService service
    )
    {
        var result = service.GetUserRoles();
        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUser(
        [FromServices] UpdateUserHandler handler, [FromBody] UpdateUserPayload payload, [FromQuery] Guid? userId
    )
    {
        var result = await handler.HandleAsync(userId, payload);
        return Ok(result);
    }
}