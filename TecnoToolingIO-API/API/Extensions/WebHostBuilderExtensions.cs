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