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
        public int AvailabilityId { get; set; }

        public int DriverId { get; set; }

        public int VehicleId { get; set; }

        public DateOnly AvailableFrom { get; set; }

        public DateOnly AvailableTo { get; set; }

        public TimeOnly SlotStart { get; set; }

        public TimeOnly SlotEnd { get; set; }

        public bool IsFullDay { get; set; }

        public bool? IsAvailable { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; } = null!;

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedOn { get; set; }
        public CreateDriverDto? Driver { get; set; }
        public CreateVehicleDto? Vehicle { get; set; }
    }
}
