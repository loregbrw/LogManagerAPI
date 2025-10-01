namespace API.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Localization;
using System.Globalization;

/// <summary>
/// Provides extension methods for configuring localization support in the application.
/// </summary>
public static class LocalizationSupportInjection
{
    /// <summary>
    /// Adds localization support to the application, including resource paths and supported cultures.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to which localization services will be added.
    /// </param>
    /// <returns>
    /// The same <see cref="IServiceCollection"/> instance, enabling fluent chaining.
    /// </returns>
    /// <remarks>
    /// The method:
    /// <list type="bullet">
    ///   <item><description>Sets the resource path for localization to "Resources".</description></item>
    ///   <item><description>Supports cultures "en-US" and "pt-BR".</description></item>
    ///   <item><description>Configures the default request culture as "en-US".</description></item>
    /// </list>
    /// </remarks>
    public static IServiceCollection AddLocalizationSupport(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        var supportedCultures = new[] { "en-US", "pt-BR" };

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("en-US");
            options.SupportedCultures = [.. supportedCultures.Select(c => new CultureInfo(c))];
            options.SupportedUICultures = [.. supportedCultures.Select(c => new CultureInfo(c))];
        });

        return services;
    }
}
