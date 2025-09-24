using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class DriverVehicleScheduleDto
    {
        public List<SelectListItem> Drivers { get; set; } = [];
        public List<SelectListItem> Vehicles { get; set; } = [];
        public List<SelectListItem> BookingStatus { get; set; } = [];
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }


}
