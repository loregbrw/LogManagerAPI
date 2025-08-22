
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

namespace API.Extensions;

using System.Net;

/// <summary>
/// Provides extension methods for configuring the web host builder.
/// </summary>
public static class WebHostBuilderExtensions
{
    /// <summary>
    /// Configures Kestrel server with IP address and port from the application configuration.
    /// Defaults to IP 127.0.0.1 and port 2525 if configuration values are missing or invalid.
    /// </summary>
    /// <param name="builder">The <see cref="ConfigureWebHostBuilder"/> to configure.</param>
    /// <param name="configuration">The application configuration providing IP and port settings.</param>
    /// <returns>The configured <see cref="ConfigureWebHostBuilder"/> instance.</returns>
    public static ConfigureWebHostBuilder ConfigureKestrel(this ConfigureWebHostBuilder builder, IConfiguration configuration)
    {
        var port = int.Parse(configuration["App:Port"] ?? "2525");
        var ip = IPAddress.Parse(configuration["App:Url"] ?? "127.0.0.1");

        builder.UseKestrel(options =>
        {
            options.Listen(ip, port);
        });

        return builder;
    }
}