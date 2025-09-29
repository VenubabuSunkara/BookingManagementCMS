using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Application.DTOs
{
    public class AssignmentViewModel
    {
        public DriverVehicleAssignmentDto Assignment { get; set; }= new DriverVehicleAssignmentDto();
        public List<SelectListItem> Drivers { get; set; }= [];
        public List<SelectListItem> Vehicles { get; set; } = [];
        public List<SelectListItem> Routes { get; set; } = [];
    }
   
}
