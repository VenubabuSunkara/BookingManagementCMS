using MimeKit.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class DriverVehicleAssignmentDto
    {
        public int Id { get; set; }

        public int DriverId { get; set; }

        public int VehicleId { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now.Date;

        public DateTime EndDate { get; set; }= DateTime.Now.Date;

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
        public int RouteId { get; set; }
       
    }
}
