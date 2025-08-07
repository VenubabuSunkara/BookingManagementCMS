using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class DriverVehicleObj
    {
        public int? DriverId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string VehicleName { get; set; } = string.Empty;
        public int SeatingCapacity { get; set; }
        public string? VehicleThumbnail { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public int? VehicleId { get; set; }
    }
    public class DriverVehicleDTable
    {
        public int Total { get; set; }
        public int Filtered { get; set; }
        public IEnumerable<DriverVehicle> driverVehicle { get; set; } = [];

    }
    public class DriverVehicle
    {
        public Driver Driver { get; set; }
        public Vehicle Vehicle { get; set; }
        public VehicleMedia VehicleMedia { get; set; }
        public DriverVehicle()
        {
            Driver = new Driver();
            Vehicle = new Vehicle();
            VehicleMedia = new VehicleMedia();
        }
    }
}
