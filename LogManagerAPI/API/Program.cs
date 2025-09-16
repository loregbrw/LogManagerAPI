using API.Extensions;
using API.Extensions.DependencyInjection;
using API.Extensions.Seeders;
using API.Middlewares;
using Application.Interfaces.Services.Core;
using Infrastructure.Data;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(builder.Configuration);

builder.Services
    .AddDatabase(builder.Configuration)
    .AddAddOns()
    .AddLocalizationSupport()
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Swagger is enabled at: http://127.0.0.1:2525/swagger");
}

Console.ForegroundColor = ConsoleColor.DarkBlue;
// Console.WriteLine("TecnoToolingIO API - Inventory Management Software");
// Console.WriteLine("Copyright (C) 2025  Lorena Gobara Falci");
// Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY.");
// Console.WriteLine("This is free software, and you are welcome to redistribute it under certain conditions.");
Console.ResetColor();

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

    await context.SeedAdminEmployeeAsync(hasher);
}

app.Run();
