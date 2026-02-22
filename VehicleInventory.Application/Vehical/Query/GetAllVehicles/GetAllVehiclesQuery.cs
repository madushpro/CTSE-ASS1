using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Repositories;

namespace VehicleInventory.Application.Vehical.Query.GetAllVehicles
{
    public class GetAllVehiclesQuery
    {
        // Optional filters
        public string? Name { get; set; }       // filter by vehicle name
        public string? Type { get; set; }       // filter by vehicle type
        public int? MinSeats { get; set; }      // minimum number of seats
        public int? MaxSeats { get; set; }      // maximum number of seats

        public GetAllVehiclesQuery()
        {
           ;
        }

    }

}