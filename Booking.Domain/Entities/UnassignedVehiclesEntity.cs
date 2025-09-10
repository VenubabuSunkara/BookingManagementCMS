using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class UnAssignedVehiclesEntity
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; } = string.Empty;
    }
}
