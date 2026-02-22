using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Application.Dto
{
    public class VehicleDto
    {
        public Guid Id { get; set; }      // Vehicle identifier
        public string Name { get; set; }   // Vehicle name
        public string Type { get; set; }   // Vehicle type (e.g., Car, Van, Truck)
        public int Seats { get; set; }     // Number of seats
    }
}
