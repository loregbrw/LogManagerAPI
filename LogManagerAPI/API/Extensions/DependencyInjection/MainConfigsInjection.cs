
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

namespace API.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for registering the main configurations and services required by the application.
/// </summary>
public static class MainConfigsInjection
{
    /// <summary>
    /// Adds core services and configurations to the application's dependency injection container,
    /// including CORS, controllers, authorization, problem details, API explorer, and Swagger.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the services will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the main configurations registered.</returns>
    public static IServiceCollection AddMainConfigs(this IServiceCollection services)
    {
        services.AddCors();

        services.AddControllers();

        services.AddAuthorization();
        services.AddProblemDetails();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}