using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class DriverVehicleAssignmentEntity
    {
        public int Id { get; set; }

        public int DriverId { get; set; }

        public int VehicleId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public int RouteId { get; set; }
    }
}
