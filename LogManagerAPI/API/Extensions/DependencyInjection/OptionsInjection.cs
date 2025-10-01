namespace API.Extensions.DependencyInjection;

using Application.Models.Options;

/// <summary>
/// Provides extension methods for configuring application options via <see cref="WebApplicationBuilder"/>.
/// </summary>
public static class OptionsInjection
{
    /// <summary>
    /// Registers configuration sections as strongly-typed options in the dependency injection container.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="WebApplicationBuilder"/> used to configure services and options.
    /// </param>
    /// <remarks>
    /// This method configures the following options:
    /// <list type="bullet">
    ///   <item><description><see cref="EmailSenderOptions"/> → bound to the "EmailSender" section of the configuration.</description></item>
    ///   <item><description><see cref="FrontendOptions"/> → bound to the "Frontend" section of the configuration.</description></item>
    /// </list>
    /// </remarks>
    public static void AddOptionsInjection(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<EmailSenderOptions>(
            builder.Configuration.GetSection("EmailSender")
        );

        builder.Services.Configure<FrontendOptions>(
            builder.Configuration.GetSection("Frontend")
        );
    }
}
