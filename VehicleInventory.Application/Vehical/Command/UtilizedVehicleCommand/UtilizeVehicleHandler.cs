using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Repositories;
using VehicleInventory.Domain.Vehical;

namespace VehicleInventory.Application.Vehical.Command.UtilizedVehicleCommand
{
    public class UtilizeVehicleHandler
    {
        private readonly IVehicleUtilizedRepository _repository;

        public UtilizeVehicleHandler(IVehicleUtilizedRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(UtilizeVehicleCommand command)
        {
            var existing = await _repository.GetByVehicleIdAsync(command.VehicleId);

            bool isOverlapping = existing.Any(x =>
                x.Overlaps(command.FromDate, command.ToDate));

            if (isOverlapping)
                return "Vehicle is already utilized for this date range.";

            var utilized = new VehicleUtilized(
                command.VehicleId,
                command.FromDate,
                command.ToDate);

            await _repository.AddAsync(utilized);

            return "Vehicle utilization created successfully.";
        }
    }
}
