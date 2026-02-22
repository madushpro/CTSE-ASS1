using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Application.Vehical.Command.UtilizedVehicleCommand
{
    public record UtilizeVehicleCommand(
     Guid VehicleId,
     DateOnly FromDate,
     DateOnly ToDate
 );
}
