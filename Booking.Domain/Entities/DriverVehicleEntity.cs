using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class DriverVehicleEntity
    {
        public DriverEntity Driver { get; set; } = new DriverEntity();
        public VehicleEntity Vehicle { get; set; } = new VehicleEntity();
    }
}
