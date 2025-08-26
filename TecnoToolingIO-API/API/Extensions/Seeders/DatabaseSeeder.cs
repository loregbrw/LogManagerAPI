
/*
    TecnoToolingIO API - Inventory Management Software with incoming and outgoing stock control.
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

namespace API.Extensions.Seeders;

using Application.Entities;
using Application.Enums;
using Application.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public static class DatabaseSeeder
{
    public static async Task SeedAdminEmployeeAsync(this TecnoToolingIODbContext context, IPasswordHasher hasher)
    {
        if (!await context.Employees.AnyAsync())
        {
            var admin = new Employee()
            {
                Code = 0,
                Name = "Admin",
                Email = "admin@tecnotooling.com",
                Password = hasher.Hash("Admin123!"),
                Role = ERole.ADMIN
            };

            context.Employees.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}