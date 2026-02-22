using Microsoft.EntityFrameworkCore;
using VehicleInventory.Domain.Vehical;

namespace VehicleInventory.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleUtilized> VehicleUtilized { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}