using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class VehicleDropdownEntity
    {
        public int VehicleId { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
    }
}
