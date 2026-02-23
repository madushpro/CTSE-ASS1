using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VehicleInventory.Application.Vehical.Command.UtilizedVehicleCommand;
using VehicleInventory.Application.Vehical.Query.GetAllVehicles;
using VehicleInventory.Application.Vehical.Query.GetAvailableVehicles;
using VehicleInventory.Domain.Repositories;
using VehicleInventory.Infrastructure.Data;
using VehicleInventory.Infrastructure.Repositories;
using System.Net;

// ⚡ Force TLS 1.2 and 1.3 for SQL Server connections
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Swagger/OpenAPI setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "VehicleInventory API",
        Version = "v1",
        Description = "API for Vehicle Inventory Management"
    });
});

// EF Core DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Scoped services
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<GetAllVehiclesHandler>();
builder.Services.AddScoped<IVehicleUtilizedRepository, VehicleUtilizedRepository>();
builder.Services.AddScoped<UtilizeVehicleHandler>();
builder.Services.AddScoped<GetAvailableVehiclesHandler>();

var app = builder.Build();

// ⚡ Apply migrations safely at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        dbContext.Database.Migrate();
        DataSeeder.Seed(dbContext); // optional seeding
    }
    catch (Exception ex)
    {
        Console.WriteLine("Database migration/connection failed: " + ex.Message);
        throw;
    }
}

// Middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleInventory API v1");
    c.RoutePrefix = "swagger";
});

// Optional redirect for /swagger/index.html
app.MapGet("/swagger/index.html", context =>
{
    context.Response.Redirect("/swagger", permanent: false);
    return Task.CompletedTask;
});

// Map controllers
app.MapControllers();

app.Run();