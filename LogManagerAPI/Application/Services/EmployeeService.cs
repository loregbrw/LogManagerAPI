namespace Application.Services;

using System.Linq;
using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces.Repositories.Primitives;
using Application.Interfaces.Services.Domain;
using Application.Services.Primitives;
using Microsoft.EntityFrameworkCore;

public class EmployeeService(
    IBaseRepository<Employee> repository
) : BaseService<Employee>(repository), IEmployeeService
{
    public async Task RegisterNewEmployee()
    {

    }

    public async Task GetPaginatedEmployees(int page, int size)
    {
        int skip = (page - 1) * size;

        var employees = await _repo.GetAllAsNoTracking()
            .OrderBy(e => e.Name)
            .Skip(skip)
            .Take(size)
            .ToListAsync();
    }

}