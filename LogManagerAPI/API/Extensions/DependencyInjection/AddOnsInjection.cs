
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

using Application.Interfaces.Providers;
using Application.Interfaces.Services.Core;
using Infrastructure.Providers;
using Infrastructure.Services;

/// <summary>
/// Provides extension methods for injecting additional services into the application's dependency injection container.
/// </summary>
public static class AddOnsInjection
{
    /// <summary>
    /// Registers additional services (such as user context, password hasher, and date/time provider)
    /// into the dependency injection container.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the services will be added.</param>
    /// <returns>The updated IServiceCollection with the added services.</returns>
    public static IServiceCollection AddAddOns(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }
}