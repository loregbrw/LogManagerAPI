
/*
    TecnoToolingIO API - Inventory Management Software with incoming and outgoing stock control.
    Copyright (C) 2025 Lorena Gobara Falci

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    Contact: loregobara@gmail.com
*/

using API.Extensions;
using API.Extensions.DependencyInjection;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(builder.Configuration);

builder.Services
    .AddDatabase(builder.Configuration)
    .AddMainConfigs();

var app = builder.Build();

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
Console.WriteLine("TecnoToolingIO API - Inventory Management Software");
Console.WriteLine("Copyright (C) 2025  Lorena Gobara Falci");
Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY.");
Console.WriteLine("This is free software, and you are welcome to redistribute it under certain conditions.");
Console.ResetColor();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<BoschBookingDbContext>();
//     var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

//     await DatabaseSeeder.SeedAsync(context, hasher);
// }

app.Run();
