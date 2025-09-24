using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class DriverVehicleSchedulerTableDto
    {
        public int TotalRecords { get; set; } = 0;
        public int FilterRecords { get; set; } = 0;
        public IEnumerable<DriverVehicleScheduleDto> DriverVehicleSchedules { get; set; } = [];
    }
}
