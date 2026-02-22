using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Application.Dto;
using VehicleInventory.Domain.Repositories;

namespace VehicleInventory.Application.Vehical.Query.GetAllVehicles
{
    public class GetAllVehiclesHandler
    {
        private readonly IVehicleRepository _repository;

        public GetAllVehiclesHandler(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<VehicleDto>> Handle(GetAllVehiclesQuery query)
        {
            var vehicles = await _repository.GetAllAsync();

            // apply filters if properties are set
            var filtered = vehicles.AsQueryable();

            if (!string.IsNullOrEmpty(query.Name))
                filtered = filtered.Where(v => v.Name.Contains(query.Name, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(query.Type))
                filtered = filtered.Where(v => v.Type.Equals(query.Type, StringComparison.OrdinalIgnoreCase));

            if (query.MinSeats.HasValue)
                filtered = filtered.Where(v => v.Seats >= query.MinSeats.Value);

            if (query.MaxSeats.HasValue)
                filtered = filtered.Where(v => v.Seats <= query.MaxSeats.Value);

            return filtered.Select(v => new VehicleDto
            {
                Id = v.Id,
                Name = v.Name,
                Type = v.Type,
                Seats = v.Seats
            }).ToList();
        }


    }
}

