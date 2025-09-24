
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class DriverRouteDto
    {
        public int Id { get; set; }
        public string? StartPlaceId { get; set; }
        public string? StartPlaceJson { get; set; }
        public string StartLocation { get; set; } = null!;

        public string EndLocation { get; set; } = null!;
        public string? EndPlaceId { get; set; }
        public string? EndPlaceJson { get; set; }
        public double Distance { get; set; }

        public TimeSpan Duration { get; set; }

        public string Status { get; set; } = null!;

        public bool IsActive { get; set; }

        public int DriverId { get; set; }

        public int VehicleId { get; set; }

        public string? RouteName { get; set; }
    }
}
