namespace API.Extensions;

using System.Net;

/// <summary>
/// Provides extension methods for configuring the web host builder.
/// </summary>
public static class WebHostBuilderExtensions
{    /// <summary>
    /// Configures the Kestrel server with a custom IP address and port
    /// based on application configuration values.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="ConfigureWebHostBuilder"/> instance being extended.
    /// </param>
    /// <param name="configuration">
    /// The application configuration that should contain:
    /// <list type="bullet">
    ///   <item><description><c>App:Port</c> → The port number to listen on.</description></item>
    ///   <item><description><c>App:Url</c> → The IP address to bind the server to.</description></item>
    /// </list>
    /// </param>
    /// <returns>
    /// The same <see cref="ConfigureWebHostBuilder"/> instance, allowing for fluent chaining.
    /// </returns>
    /// <remarks>
    /// If either the IP or port configuration is missing or invalid,
    /// the Kestrel server will not be explicitly configured and will fall back to defaults.
    /// </remarks>
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