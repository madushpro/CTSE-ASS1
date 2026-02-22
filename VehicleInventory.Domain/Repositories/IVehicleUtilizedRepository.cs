using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Vehical;

namespace VehicleInventory.Domain.Repositories
{
    public interface IVehicleUtilizedRepository
    {
        Task<List<VehicleUtilized>> GetByVehicleIdAsync(Guid vehicleId);
        Task AddAsync(VehicleUtilized utilized);
    }
}
