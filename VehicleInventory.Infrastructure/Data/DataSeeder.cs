using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Vehical;

namespace VehicleInventory.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Vehicles.Any())
            {
                var vehicles = new List<Vehicle>
        {
            new Vehicle(Guid.Parse("45C20120-521A-442F-BBCE-47867B29B6A3"), "Toyota Corolla", "Car", 5),
            new Vehicle(Guid.Parse("3C5742D5-6F10-4405-9D11-52899E562457"), "Scania R500", "Truck", 2),
            new Vehicle(Guid.Parse("41FCC791-7C74-4518-8A74-63A7384DCE7D"), "Honda Civic", "Car", 5),
            new Vehicle(Guid.Parse("BD6F1143-DFAC-45BE-A7CF-81F0CF75B0F2"), "Ford Transit", "Van", 12),
            new Vehicle(Guid.Parse("3827C23C-A7C2-49E7-BEA7-C621BD8486B8"), "Mercedes Sprinter", "Van", 15),
            new Vehicle(Guid.Parse("7047F0A9-3980-4D59-BF73-EC2504A11B27"), "Volvo FH", "Truck", 2)
        };

                context.Vehicles.AddRange(vehicles);
                context.SaveChanges();
            }

            // ---- VEHICLE UTILIZED SEED ----
            if (!context.VehicleUtilized.Any())
            {
                var utilized = new List<VehicleUtilized>
        {
            new VehicleUtilized(
                Guid.Parse("45C20120-521A-442F-BBCE-47867B29B6A3"),
                new DateOnly(2026, 3, 1),
                new DateOnly(2026, 3, 5)
            ),

            new VehicleUtilized(
                Guid.Parse("BD6F1143-DFAC-45BE-A7CF-81F0CF75B0F2"),
                new DateOnly(2026, 3, 10),
                new DateOnly(2026, 3, 15)
            ),

            new VehicleUtilized(
                Guid.Parse("7047F0A9-3980-4D59-BF73-EC2504A11B27"),
                new DateOnly(2026, 3, 3),
                new DateOnly(2026, 3, 7)
            )
        };

                context.VehicleUtilized.AddRange(utilized);
                context.SaveChanges();
            }
        }
    }
}
