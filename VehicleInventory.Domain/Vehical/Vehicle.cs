using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Domain.Vehical
{
    public class Vehicle
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public int Seats { get; private set; }

        private Vehicle() { } // EF Core

        public Vehicle(Guid id, string name, string type, int seats)
        {
            Id = id;
            Name = name;
            Type = type;
            Seats = seats;
        }
    }
}
