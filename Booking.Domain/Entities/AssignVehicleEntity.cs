using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class AssignVehicleDriverEntity
    {
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
}
