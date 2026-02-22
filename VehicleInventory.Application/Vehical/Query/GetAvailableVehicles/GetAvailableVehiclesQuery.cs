using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Application.Vehical.Query.GetAvailableVehicles
{
    public record GetAvailableVehiclesQuery(
     DateOnly FromDate,
     DateOnly ToDate
 );
}
