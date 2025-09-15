using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class DriverVehicleAvailabilityEntity
    {
        public int AvailabilityId { get; set; }
        public int? DriverId { get; set; }
        public int? VehicleId { get; set; }
        public DateOnly AvailableDate { get; set; }
        public TimeOnly SlotStart { get; set; }
        public TimeOnly SlotEnd { get; set; }
    }
}
