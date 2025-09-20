namespace API.Extensions;

using System.Net;

/// <summary>
/// Provides extension methods for configuring the web host builder.
/// </summary>
public static class WebHostBuilderExtensions
{
    /// <summary>
    /// Configures Kestrel server with IP address and port from the application configuration.
    /// </summary>
    /// <param name="builder">The <see cref="ConfigureWebHostBuilder"/> to configure.</param>
    /// <param name="configuration">The application configuration providing IP and port settings.</param>
    /// <returns>The configured <see cref="ConfigureWebHostBuilder"/> instance.</returns>
    public static ConfigureWebHostBuilder ConfigureKestrel(this ConfigureWebHostBuilder builder, IConfiguration configuration)
    {
        var hasPortConfig = int.TryParse(configuration["App:Port"], out var port);
        var hasIpConfig = IPAddress.TryParse(configuration["App:Url"], out var ip);

        if (hasPortConfig && hasIpConfig)
        {
            builder.UseKestrel(options =>
            {
                options.Listen(ip!, port);
            });
        }

        return builder;
    }
}