namespace API.Extensions.DependencyInjection;

using Application.Interfaces.Providers;
using Application.Interfaces.Services.Core;
using Application.Interfaces.Services.Core.Auth;
using Infrastructure.Providers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;

public static class AddOnsInjection
{
    public static IServiceCollection AddAddOns(this IServiceCollection services)
    {
        services.AddScoped<IUserContext, UserContext>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddScoped<ICsvService, CsvService>();
        services.AddScoped<IEnumHelper, EnumHelper>();
        services.AddScoped<IEmailSenderService, EmailSenderService>();
        services.AddScoped<IEmailTemplateHelper, EmailTemplateHelper>();

        return services;
    }
}