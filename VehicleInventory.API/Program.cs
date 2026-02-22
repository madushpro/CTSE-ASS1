using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VehicleInventory.Application.Vehical.Command.UtilizedVehicleCommand;
using VehicleInventory.Application.Vehical.Query.GetAllVehicles;
using VehicleInventory.Application.Vehical.Query.GetAvailableVehicles;
using VehicleInventory.Domain.Repositories;
using VehicleInventory.Infrastructure.Data;
using VehicleInventory.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Swagger/OpenAPI
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

// Apply migrations + seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
    DataSeeder.Seed(dbContext);
}

// Middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// ⚡ Swagger UI setup
app.UseSwagger(); // generate swagger JSON
app.UseSwaggerUI(c =>
{
    // Swagger JSON location
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleInventory API v1");

    // Serve UI at /swagger
    c.RoutePrefix = "swagger"; // Swagger UI is now at /swagger
});

// Optional: redirect /swagger/index.html → /swagger
app.MapGet("/swagger/index.html", context =>
{
    context.Response.Redirect("/swagger", permanent: false);
    return Task.CompletedTask;
});

// Map controllers
app.MapControllers();

app.Run();