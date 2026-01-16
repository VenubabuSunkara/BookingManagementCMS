using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public class UnassignedVehiclesModel
    {
        public IEnumerable<SelectListItem> UnassignedVehicles = [];
        public int DriverId { get; set; }
        public int VehicleId { get; set; }
    }
}
