using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class DriverVehicleDto
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string DriverContact { get; set; } = string.Empty;
        public string VehicleName { get; set; } = string.Empty;
        public int SeatingCapacity { get; set; }
        public string VehicleThumbnail { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public int VehicleId { get; set; }
        public DateTime Created { get;set; }
    }
}
