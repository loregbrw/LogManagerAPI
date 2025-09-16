namespace API.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Localization;
using System.Globalization;

public static class LocalizationSupportInjection
{
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
