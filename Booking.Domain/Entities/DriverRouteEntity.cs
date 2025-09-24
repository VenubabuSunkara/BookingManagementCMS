using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class DriverRouteEntity
    {
        public int Id { get; set; }

        public string StartLocation { get; set; } = null!;

        public string EndLocation { get; set; } = null!;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double Distance { get; set; }

        public string Status { get; set; } = null!;

        public bool IsActive { get; set; }

        public int DriverId { get; set; }

        public int VehicleId { get; set; }

        public string? RouteName { get; set; }

    }
}
