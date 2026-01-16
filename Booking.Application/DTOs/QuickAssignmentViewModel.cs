using Booking.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class QuickAssignmentViewModel
    {
        public DriverVehicleAssignmentDto Assignment { get; set; } = new DriverVehicleAssignmentDto();
        public DriverDto? Driver { get; set; } = new DriverDto();
        public VehicleDto? Vehicle { get; set; } = new VehicleDto();
        public int TripTypeId { get; set; }
        public int VehicleTypeId { get; set; }
        public List<SelectListItem> VehicleType { get; set; } = [];
        public List<SelectListItem> TripType { get; set; } = [];
        public List<DriverRouteDto> Routes { get; set; } = [];
        public List<SelectListItem> Drivers { get; set; } = [];
        public List<SelectListItem> Vehicles { get; set; } = [];
    }
}
