
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

namespace API.Extensions.DependencyInjection;

using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Provides extension methods for registering the application's database context and related services.
/// </summary>
public static class DatabaseInjection
{
    /// <summary>
    /// Registers the <see cref="TecnoToolingIODbContext"/> using the PostgreSQL provider with the connection string
    /// from the configuration.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the database context will be added.</param>
    /// <param name="configuration">The ConfigurationManager used to retrieve the connection string.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the database context registered.</returns>
    public static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<TecnoToolingIODbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}