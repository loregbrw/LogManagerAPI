namespace API.Extensions;

using System.Net;

public static class WebHostBuilderExtensions
{
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