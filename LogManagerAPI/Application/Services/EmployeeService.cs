
/*
    LogManager API
 - Inventory Management Software with incoming and outgoing stock control.
    Copyright (C) 2025 Lorena Gobara Falci

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    Contact: loregobara@gmail.com
*/

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