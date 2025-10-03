namespace API.Extensions.DependencyInjection;

using Application.Models.Options;

public static class OptionsInjection
{
    public static void AddOptionsInjection(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AdminUserOptions>(
            builder.Configuration.GetSection("AdminUser")
        );

        builder.Services.Configure<EmailSenderOptions>(
            builder.Configuration.GetSection("EmailSender")
        );

        builder.Services.Configure<FrontendOptions>(
            builder.Configuration.GetSection("Frontend")
        );
    }
}
