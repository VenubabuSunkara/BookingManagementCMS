using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class UnAssignedVehiclesDto
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
    }
}
