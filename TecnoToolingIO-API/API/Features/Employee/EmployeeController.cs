namespace API.Features.Employee;

using API.Attributes;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/employee")]
public class EmployeeController : ControllerBase
{
    // [ManagerAuthentication]
    // [HttpGet]
    // public async Task<IActionResult> GetPaginatedEmployees(
    //     [FromQuery] int? page, [FromQuery] int? count
    // )
    // {

    // }
}