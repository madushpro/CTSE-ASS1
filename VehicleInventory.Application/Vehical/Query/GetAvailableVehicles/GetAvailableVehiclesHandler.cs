using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Repositories;
using VehicleInventory.Domain.Vehical;

namespace VehicleInventory.Application.Vehical.Query.GetAvailableVehicles
{
    public class GetAvailableVehiclesHandler
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleUtilizedRepository _utilizedRepository;

        public GetAvailableVehiclesHandler(
            IVehicleRepository vehicleRepository,
            IVehicleUtilizedRepository utilizedRepository)
        {
            _vehicleRepository = vehicleRepository;
            _utilizedRepository = utilizedRepository;
        }

        public async Task<List<Vehicle>> Handle(GetAvailableVehiclesQuery query)
        {
            if (query.ToDate < query.FromDate)
                throw new ArgumentException("Invalid date range");

            var allVehicles = await _vehicleRepository.GetAllAsync();

            var result = new List<Vehicle>();

            foreach (var vehicle in allVehicles)
            {
                var utilizedDates = await _utilizedRepository
                    .GetByVehicleIdAsync(vehicle.Id);

                bool isOverlapping = utilizedDates.Any(x =>
                    x.Overlaps(query.FromDate, query.ToDate));

                if (!isOverlapping)
                {
                    result.Add(vehicle);
                }
            }

            return result;
        }
    }
}
