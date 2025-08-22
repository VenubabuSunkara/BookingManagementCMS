
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class VehicleDto
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string? VehicleName { get; set; }

        public string? VehicleNumber { get; set; }

        public int? VehicleTypeId { get; set; }

        public string? Features { get; set; }

        public string? AboutOnVehicle { get; set; }

        public int? SeatingCapacity { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? Color { get; set; }

        public string? Make { get; set; }

        public string? Model { get; set; }
    }
}
