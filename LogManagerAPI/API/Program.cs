using API.Extensions;
using API.Extensions.DependencyInjection;
using API.Extensions.Seeders;
using API.Middlewares;
using Application.Interfaces.Providers;
using Application.Interfaces.Services.Core.Auth;
using Application.Models.Options;
using Infrastructure.Data;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(builder.Configuration);

builder.Services
    .AddDatabase(builder.Configuration)
    .AddAddOns()
    .AddLocalizationSupport()
    .AddRepositories()
    .AddMappers()
    .AddServices()
    .AddMainConfigs();

builder.AddOptionsInjection();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var app = builder.Build();

app.UseRequestLocalization(
    app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value
);

string environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
Console.ForegroundColor = app.Environment.IsDevelopment() ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;
Console.WriteLine($"Environment: {environment}");

Console.ForegroundColor = ConsoleColor.DarkBlue;
Console.WriteLine("LogManager API - Logistics Management Software");
Console.WriteLine("Copyright (c) 2025 Lorena Gobara Falci");
Console.ResetColor();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LogManagerDbContext>();
    var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
    var dateTimeProvider = scope.ServiceProvider.GetRequiredService<IDateTimeProvider>();

    var adminOptions = scope.ServiceProvider.GetRequiredService<IOptions<AdminUserOptions>>();

    await context.SeedAdminUserAsync(hasher, dateTimeProvider, adminOptions);
}

app.Run();
