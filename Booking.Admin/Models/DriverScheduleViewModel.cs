using Booking.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public class DriverScheduleViewModel
    {
        public List<SelectListItem> Drivers { get; set; } = [];
        public List<SelectListItem> Vehicles { get; set; } = [];
        public List<SelectListItem> BookingStatus { get; set; } = [];
        public int? DriverId { get; set; }
        public int? VehicleId { get; set; }
        public int? BookingStatusId { get; set; }
        public IEnumerable<DriverVehicleScheduleDto> ScheduleDtos { get; set; } = [];
    }
}
