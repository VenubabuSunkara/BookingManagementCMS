using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs.Tour
{
    public class PackageDropdownDto
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; } = string.Empty;
    }
}
