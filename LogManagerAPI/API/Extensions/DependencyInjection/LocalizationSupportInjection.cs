namespace API.Extensions.DependencyInjection;

public static class LocalizationSupportInjection
{
    public static IServiceCollection AddLocalizationSupport(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resources");

        var supportedCultures = new[] { "en-US", "pt-BR" };

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
        });

        return services;
    }
}
