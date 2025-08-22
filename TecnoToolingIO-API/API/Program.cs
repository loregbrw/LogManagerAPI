
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(builder.Configuration);

// builder.Services
//     .AddAppServices()

var app = builder.Build();

string environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
Console.WriteLine($"Environment: {environment}");

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
app.MapControllers();

// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<BoschBookingDbContext>();
//     var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

//     await DatabaseSeeder.SeedAsync(context, hasher);
// }


app.Run();
