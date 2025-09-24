using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs
{
    public class DriversDropdownDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string License { get; set; } = string.Empty;
    }
}
