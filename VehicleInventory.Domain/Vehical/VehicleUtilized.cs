using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Domain.Vehical
{
    public class VehicleUtilized
    {
        public Guid Id { get; private set; }
        public Guid VehicleId { get; private set; }
        public DateOnly FromDate { get; private set; }
        public DateOnly ToDate { get; private set; }

        private VehicleUtilized() { } // EF Core

        public VehicleUtilized(Guid vehicleId, DateOnly fromDate, DateOnly toDate)
        {
            if (toDate < fromDate)
                throw new ArgumentException("ToDate cannot be earlier than FromDate");

            Id = Guid.NewGuid();
            VehicleId = vehicleId;
            FromDate = fromDate;
            ToDate = toDate;
        }

        public bool Overlaps(DateOnly from, DateOnly to)
        {
            return FromDate <= to && ToDate >= from;
        }
    }
}
