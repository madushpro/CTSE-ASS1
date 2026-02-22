using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Repositories;
using VehicleInventory.Domain.Vehical;
using VehicleInventory.Infrastructure.Data;

namespace VehicleInventory.Infrastructure.Repositories
{
    public class VehicleUtilizedRepository : IVehicleUtilizedRepository
    {
        private readonly AppDbContext _context;

        public VehicleUtilizedRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleUtilized>> GetByVehicleIdAsync(Guid vehicleId)
        {
            return await _context.VehicleUtilized
                .Where(x => x.VehicleId == vehicleId)
                .ToListAsync();
        }

        public async Task AddAsync(VehicleUtilized utilized)
        {
            await _context.VehicleUtilized.AddAsync(utilized);
            await _context.SaveChangesAsync();
        }
    }
}
