using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class AssignmentViewModel
    {
        public DriverVehicleAssignmentDto Assignment { get; set; }
        public List<SelectListItem> Drivers { get; set; }
        public List<SelectListItem> Vehicles { get; set; }
        public List<SelectListItem> Routes { get; set; }
    }
   
}
