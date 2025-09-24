using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class DriverVehicleSchedulerTableFilterDto
    {
        public int VehicleId { get; set; }
        public int DriverId {  get; set; }
        public string? BookingStatus {  get; set; }
    }
}
