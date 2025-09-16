using Application.Models.Options;

namespace API.Extensions.DependencyInjection;

public static class OptionsInjection
{
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