namespace API.Features.UserDepartment;

using API.Attributes;
using API.Features.User.Get;
using API.Features.User.Post;
using Application.Interfaces.Services.Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/user-departments")]
public class UserDepartmentController : ControllerBase
{
    [HttpGet("values")]
    public async Task<IActionResult> GetUserDepartments(
        [FromServices] IUserDepartmentService service
    )
    {
        var response = await service.GetUserDepartmentValues();
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