using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class VehicleDropdownDto
    {
        public int VehicleId { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
    }
}
