using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Models
{
    public class UnassignedDriversModel
    {
        public IEnumerable<SelectListItem> UnassignedDrivers = [];
        public int VehicleId { get; set; }  
    }
}
