using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class DriverVehicleExportEntity
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? LicenseNumber { get; set; }

        public string? Address { get; set; }
        public string? AboutOn { get; set; }
        public bool? ApproveDriver { get; set; }
        public bool? AvailabilityStatus { get; set; }
        public string? Description { get; set; }

        public string? VehicleName { get; set; }

        public string? VehicleNumber { get; set; }

        public int? VehicleTypeId { get; set; }

        public string? Features { get; set; }

        public string? AboutOnVehicle { get; set; }

        public int? SeatingCapacity { get; set; }
        public string? Color { get; set; }

        public string? Make { get; set; }

        public string? Model { get; set; }
    }
}